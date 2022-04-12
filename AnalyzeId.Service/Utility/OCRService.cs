using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.Textract;
using Amazon.Textract.Model;
using AnalyzeId.Shared;
using AnalyzeId.Shared.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NLog;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeId.Service.Utility
{
    public class OCRService : IOCRService
    {
        private readonly IFileUploader fileUploader;
        private readonly IHostingEnvironment hostingEnvironment;
        private ILogger logger = LogManager.GetCurrentClassLogger();
        public OCRService(IFileUploader fileUploader, IHostingEnvironment hostingEnvironment)
        {
            this.fileUploader = fileUploader;
            this.hostingEnvironment = hostingEnvironment;
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
        public async Task<OperationResult<FileUploadPathDTO>> UploadImage(IFormFile file)
        {
            try
            {

                if (file != null)
                {
                    var filePath = await fileUploader.CheckAndUploadAsync(file);
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
                return new OperationResult<FileUploadPathDTO> { };
            }
            catch (Exception ex)
            {
                logger.Debug(ex);
                return OperationResult<FileUploadPathDTO>.Error(ex);
            }
        }

        public async Task<OperationResult<FinalResultOCRDTO>> GetOCRResult(string fileFrontPath, string fileBackPath)
        {
            try
            {
                var IDVTask = _GetOCRResult(fileFrontPath, fileBackPath);
                var awsTask = _GetOCRResultByAmazonApi(fileFrontPath);
                await Task.WhenAll(IDVTask, awsTask);
                awsTask.Result.FrontUrl = fileFrontPath;
                awsTask.Result.BackUrl = fileBackPath;

                IDVTask.Result.Data.FrontUrl = fileFrontPath;
                IDVTask.Result.Data.BackUrl = fileBackPath;


                if (IDVTask.Result.Succeed == true || (IDVTask.Result.Data.FullName.HasValue()   && IDVTask.Result.Data.Birth_Date.HasValue()))
                {
                    return new OperationResult<FinalResultOCRDTO> { Data = IDVTask.Result.Data, Message = IDVTask.Result.Message, Succeed = IDVTask.Result.Succeed };
                }
                return new OperationResult<FinalResultOCRDTO> { Data = awsTask.Result, Message = IDVTask.Result.Message, Succeed = IDVTask.Result.Succeed };
                
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
                            FullName = result?.Result?.Data?.Full_Name,
                            //Middle_Name = result?.Result?.Data?.Middle_Name,
                            Card_Number = result?.Result?.Data?.Card_Number,
                            Class_Code = result?.Result?.Data?.Class_Code,
                            Licence_Type_Name = result?.Result?.Data?.Licence_Type_Name,
                            Condition_Code = result?.Result?.Data?.Condition_Code,
                            Address_Postal_Code = result?.Result?.Data?.Address_Postal_Code,
                            Address =  result?.Result?.Data?.Address,
                            Document_Number = result?.Result?.Data?.Document_Number,
                            Licence_Type_Code = result?.Result?.Data?.Licence_Type_Code,
                            Template_Name = result?.Result?.Data?.Template_Name,
                            Unit_Number = result?.Result?.Data?.Unit_Number,
                            Birth_Date = result?.Result?.Data?.Birth_Date,
                            Expiry_Date = result?.Result?.Data?.Expiry_Date,


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
                            FullName = x[1].value + " " + x[2].value+ " " + x[3].value,
                            //Middle_Name = x[2].value,
                            Card_Number = x[8].value,
                            Class_Code = x[16].value,
                            Licence_Type_Name = x[12].value,
                            //Condition_Code =  ,
                            Address_Postal_Code = x[5].value,
                            Address =   x[17].value +" "+x[4].value+" "+x[5].value,
                            Document_Number = x[8].value,
                            //Licence_Type_Code = oCRDTO?.Result?.Data?.Licence_Type_Code,
                            //Template_Name = oCRDTO?.Result?.Data?.Template_Name,
                            //Unit_Number = oCRDTO?.Result?.Data?.Unit_Number,
                            Birth_Date = x[10].value,
                            Expiry_Date = x[9].value,


                        };
                    }
                }

            }
            return null;
        }

    }
}