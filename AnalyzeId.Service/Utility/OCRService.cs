﻿using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.Textract;
using Amazon.Textract.Model;
using AnalyzeId.Domain.Model;
using AnalyzeId.Domain.ViewModel;
using AnalyzeId.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeId.Service.Utility
{
    public class OCRService : IOCRService
    {
        private readonly IFileUploader fileUploader;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IConfiguration configuration;
        private readonly IImagePassportUrlRepository passportUrlRepository;
        private ILogger logger = LogManager.GetCurrentClassLogger();
        public OCRService(IFileUploader fileUploader, IHostingEnvironment hostingEnvironment, IConfiguration configuration, IImagePassportUrlRepository passportUrlRepository)
        {
            this.fileUploader = fileUploader;
            this.hostingEnvironment = hostingEnvironment;
            this.configuration = configuration;
            this.passportUrlRepository = passportUrlRepository;
        }
        public async Task<OperationResult<FileUploadPathDTO>> UploadImage(string file)
        {
            try
            {
                var filePath = await fileUploader.CheckAndUploadBase64Async(file);
                //logger.Debug("base64: " + file);

                var fullPath = Path.Combine(hostingEnvironment.WebRootPath, filePath.TrimStart('/', '\\'));
                return new OperationResult<FileUploadPathDTO>
                {
                    Succeed = true,
                    Message = OperationResult<object>.SuccessMessage,
                    Data = new FileUploadPathDTO
                    {
                        Path = filePath,
                        FullPath = fullPath,
                    }
                };

            }
            catch (Exception ex)
            {
                logger.Debug(ex);
                return OperationResult<FileUploadPathDTO>.Error(ex);
            }
        }
        public async Task<OperationResult<FileUploadPathDTO>> UploadImage(IFormFile file, Guid id, bool isFront)
        {
            try
            {

                if (file != null)
                {
                    var filePath = await fileUploader.CheckAndUploadAsync(file);
                    var fullPath = Path.Combine(hostingEnvironment.WebRootPath, filePath.TrimStart('/', '\\'));
                    await passportUrlRepository.Update(id, filePath, isFront);
                    return new OperationResult<FileUploadPathDTO>
                    {
                        Succeed = true,
                        Message = OperationResult<object>.SuccessMessage,
                        Data = new FileUploadPathDTO
                        {
                            Path = filePath,
                            FullPath = fullPath,
                        }
                    };
                }
                return new OperationResult<FileUploadPathDTO> { };
            }
            catch (Exception ex)
            {
                logger.Debug(ex);
                return OperationResult<FileUploadPathDTO>.Error(ex);
            }
        }

        public async Task<OperationResult<FinalResultOCRDTO>> GetOCRResult(Guid Id)
        {
            try
            {
                var time = DateTime.Now;
                var imgPassport = await passportUrlRepository.Get(Id);
                if (string.IsNullOrEmpty(imgPassport?.FrontUrl))
                {
                    while (true)
                    {
                        System.Threading.Thread.Sleep(3000);
                        imgPassport = await passportUrlRepository.Get(Id);
                        if (imgPassport?.FrontUrl != null )
                        {
                            break;
                        }else if (time.AddMinutes(1) < DateTime.Now)
                        {
                            return new OperationResult<FinalResultOCRDTO> { Data = null, Message ="Please try again", Succeed =false};
                        }
                    }
                }
                imgPassport.FrontUrl = imgPassport.FrontUrl.PathToUrl();
                imgPassport.BackUrl =string.IsNullOrEmpty(imgPassport.BackUrl)? null: imgPassport.BackUrl.PathToUrl();

                var IDVTask = _GetOCRResult(imgPassport.FrontUrl, imgPassport.BackUrl);
                var awsTask = _GetOCRResultByAmazonApi(imgPassport.FrontUrl);
                await Task.WhenAll(IDVTask, awsTask);
                awsTask.Result.FrontUrl = imgPassport.FrontUrl;
                awsTask.Result.BackUrl = imgPassport.BackUrl;
                if (IDVTask.Result.Data!=null)
                {
       IDVTask.Result.Data.FrontUrl = imgPassport.FrontUrl;
                IDVTask.Result.Data.BackUrl = imgPassport.BackUrl;   var faceUrl = _GetOCRImage(IDVTask.Result.Data.ImageFaseId, IDVTask.Result.Data.TransactionId);
                var signatureUrl = _GetOCRImage(IDVTask.Result.Data.ImageSignatureId, IDVTask.Result.Data.TransactionId);
                await Task.WhenAll(faceUrl, signatureUrl);
                IDVTask.Result.Data.FaceUrl = faceUrl.Result;
                IDVTask.Result.Data.SignatureUrl = signatureUrl.Result;
                    IDVTask.Result.Data.BackUrl = imgPassport.FrontUrl;
                    IDVTask.Result.Data.BackUrl = imgPassport.BackUrl;
                }
         

                //if (IDVTask.Result.Data.ImageFaseId!=null)
                //{
                //    IDVTask.Result.Data.ImageFaseId= await _GetOCRImage(IDVTask.Result.Data.ImageFaseId, IDVTask.Result.Data.TransactionId);
                //}
             

             

                return new OperationResult<FinalResultOCRDTO> { Data = ComoareResult(IDVTask.Result.Data, awsTask.Result), Message = IDVTask.Result.Message, Succeed = IDVTask.Result.Succeed };

                //if (IDVTask.Result.Succeed == true || (IDVTask.Result.Data.FullName.HasValue() && IDVTask.Result.Data.BirthDate.HasValue()))
                //{
                //    return new OperationResult<FinalResultOCRDTO> { Data = IDVTask.Result.Data, Message = IDVTask.Result.Message, Succeed = IDVTask.Result.Succeed };
                //}
                //return new OperationResult<FinalResultOCRDTO> { Data = awsTask.Result, Message = IDVTask.Result.Message, Succeed = IDVTask.Result.Succeed };
            }
            catch (Exception ex)
            {
                logger.Debug(ex);
                return OperationResult<FinalResultOCRDTO>.Error(ex);
            }
        }

        private async Task<OperationResult<FinalResultOCRDTO>> _GetOCRResult(string fileFrontPath, string fileBackPath)
        {
            try
            {
                var client = new RestClient("https://services.idvpacific.com.au/api/Request/OCR-ID-PREMIUM");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("API-Username", "hamid");
                request.AddHeader("API-Password", "5150Murphy.mo71*");
                request.AddParameter("Date_Format", "yyyy-mm-dd");
                request.AddParameter("Processing_Type", "6");
                request.AddParameter("Document_Validation", "True");
                request.AddParameter("Overlay_Required", "True");
                request.AddParameter("Detect_Orientation", "True");
                request.AddParameter("Image_Scale", "False");
                request.AddParameter("Cropping_Mode", "True");
                request.AddFile("ID_Front_Image", fileFrontPath);
                if (fileBackPath != null)
                {
                    request.AddFile("ID_Back_Image", fileBackPath);
                }

                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var result = System.Text.Json.JsonSerializer.Deserialize<OCRDTO>(response.Content);

                    return new OperationResult<FinalResultOCRDTO>
                    {
                        Succeed = true,
                        Data = new FinalResultOCRDTO
                        {
                            FullName = result?.Result?.Data?.First_Name + " " + result?.Result?.Data?.Given_Name,
                            MiddleName = result?.Result?.Data?.Middle_Name,
                            FirstName = result?.Result?.Data?.First_Name,
                            Surname = result?.Result?.Data?.Given_Name,
                            DocumentNumber = result?.Result?.Data?.Document_Number,
                            BirthDate = result?.Result?.Data?.Birth_Date,
                            ExpiryDate = result?.Result?.Data?.Expiry_Date,
                            Address = result?.Result?.Data?.Address,
                            ImageBackId = result?.Result?.Files?.Processed?.Back_image,
                            ImageFrontId = result?.Result?.Files?.Processed?.Front_image,
                            ImageSignatureId = result?.Result?.Files?.Processed?.Signature,
                            ImageFaseId = result?.Result?.Files?.Processed?.Face,
                            TransactionId = result?.Result?.Transaction?.ID,
                            JsonResultIDv = response.Content,

                        },

                        Message = OperationResult<int>.SuccessMessage
                    };
                }
                return new OperationResult<FinalResultOCRDTO>
                {
                    Succeed = false,
                    Message = OperationResult<int>.ErrorMessage,
                };


            }
            catch (Exception ex)
            {
                logger.Debug(ex);
                return OperationResult<FinalResultOCRDTO>.Error(ex);
            }
        }
        public async Task<FinalResultOCRDTO> _GetOCRResultByAmazonApi(string path)
        {
            string accessKey = "AKIASMEH2HYLHKKBFNHR", secretAccessKey = "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";
            var file = File.OpenRead(path);

            using (var client = new AmazonS3Client(accessKey, secretAccessKey, Amazon.RegionEndpoint.APSoutheast2))
            {
                var transfer = new TransferUtility(client);
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);

                    //await transfer.UploadAsync(stream, bucketname, "myfile");
                    using (var textractClient = new AmazonTextractClient(accessKey, secretAccessKey, Amazon.RegionEndpoint.APSoutheast2))
                    {
                        var request = new AnalyzeIDRequest
                        {
                            DocumentPages = new List<Document>
                            {
                                new Document
                            {
                                Bytes = ms,
                            },
                            }

                        };
                        var res = await textractClient.AnalyzeIDAsync(request);
                        var x = res.IdentityDocuments.FirstOrDefault().IdentityDocumentFields.Select(s => new { key = s.Type.Text, value = s.ValueDetection.Text }).ToList();
                        return new FinalResultOCRDTO
                        {
                            FullName = x[0].value + " " + x[1].value,
                            MiddleName = x[2].value,
                            Surname = x[1].value,
                            FirstName = x[0].value,
                            Address = x[17].value,
                            DocumentNumber = x[8].value,
                            BirthDate = x[10].value,
                            ExpiryDate = x[9].value,
                        };
                    }
                }

            }
        }

        public FinalResultOCRDTO ComoareResult(FinalResultOCRDTO finalResultIdv, FinalResultOCRDTO finalResultAZ)
        {
            finalResultIdv= finalResultIdv == null ? new FinalResultOCRDTO { } : finalResultIdv;
           
            finalResultIdv.FirstName = !finalResultIdv.FirstName.HasValue() ? finalResultAZ?.FirstName : finalResultIdv?.FirstName;
            finalResultIdv.MiddleName = !finalResultIdv.MiddleName.HasValue() ? finalResultAZ?.MiddleName : finalResultIdv?.MiddleName;
            finalResultIdv.Surname = !finalResultIdv.Surname.HasValue() ? finalResultAZ?.Surname : finalResultIdv?.Surname;
            finalResultIdv.FullName = !finalResultIdv.FullName.HasValue() ? finalResultAZ?.FullName : finalResultIdv?.FullName;
            finalResultIdv.Address = !finalResultIdv.Address.HasValue() ? finalResultAZ?.Address : finalResultIdv?.Address;
            finalResultIdv.BirthDate = !finalResultIdv.BirthDate.HasValue() ? finalResultAZ?.BirthDate : finalResultIdv?.BirthDate;
            finalResultIdv.ExpiryDate = !finalResultIdv.ExpiryDate.HasValue() ? finalResultAZ?.ExpiryDate : finalResultIdv?.ExpiryDate;
            finalResultIdv.DocumentNumber = !finalResultIdv.DocumentNumber.HasValue() ? finalResultAZ?.DocumentNumber : finalResultIdv?.DocumentNumber;

            //finalResultIdv.BackUrl = finalResultIdv?.BackUrl;
            //finalResultIdv.ImageBackId = finalResultIdv?.ImageBackId;

            //finalResultIdv.FrontUrl = finalResultIdv?.FrontUrl;
            //finalResultIdv.ImageFrontId = finalResultIdv?.ImageFrontId;

            //finalResultIdv.FaceUrl = finalResultIdv?.FaceUrl;
            //finalResultIdv.ImageFaseId = finalResultIdv?.ImageFaseId;

            //finalResultIdv.SignatureUrl = finalResultIdv?.SignatureUrl;
            //finalResultIdv.ImageSignatureId = finalResultIdv?.ImageSignatureId;

            //finalResultIdv.JsonResultIDv = finalResultIdv?.JsonResultIDv;
            //finalResultIdv.TransactionId = finalResultIdv?.TransactionId;

            return finalResultIdv;
        }


        private async Task<string> _GetOCRImage(string imageId, string transactionId)
        {
            try
            {
                if (imageId == null || transactionId == null)
                {
                    return null;
                }
                var client = new RestClient("https://services.idvpacific.com.au/api/Result/RetrieveFile");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("API-Username", "hamid");
                request.AddHeader("API-Password", "5150Murphy.mo71*");
                request.AddParameter("Transaction_ID", transactionId);
                request.AddParameter("Image_ID", imageId);
                request.AddParameter("Username", "hamid");
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var paths = Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\" + Guid.NewGuid().ToString() + ".jpg";
                    File.WriteAllBytes(paths, response.RawBytes);
                    return paths;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.Debug(ex);
                return null;
            }
        }
        public void SendRequestToWebhook(string transactionId)
        {
            try
            {
                var model = new SendRequestToWebhookViewModel { Message = "The request was successful", Transaction_ID = transactionId, Succeed = true };
                var webhook = configuration.GetSection("Webhook").Value;
                var client = new RestClient(webhook);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                var data = JsonConvert.SerializeObject(model);
                request.AddParameter("Result", JsonConvert.SerializeObject(model));
                IRestResponse response = client.Execute(request);
            }
            catch (Exception ex)
            {
                logger.Debug(ex);
            }
        }
        public string SaveImageBase64(string base64, string type)
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\" + Guid.NewGuid().ToString() + "." + type.Remove(0, type.IndexOf("/") + 1));
                System.IO.File.WriteAllBytes(fullPath, Convert.FromBase64String(base64));
                return fullPath;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

    }
}