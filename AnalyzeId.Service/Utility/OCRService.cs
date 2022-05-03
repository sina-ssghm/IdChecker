using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.Textract;
using Amazon.Textract.Model;
using AnalyzeId.Domain.Model;
using AnalyzeId.Domain.ViewModel;
using AnalyzeId.Domain.ViewModel.CreateApplicationViewModel;
using AnalyzeId.Domain.ViewModel.CreateElement;
using AnalyzeId.Domain.ViewModel.UploadFile;
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
        public async Task<string> UploadImage(IFormFile file)
        {
            try
            {

                if (file != null)
                {
                    var filePath = await fileUploader.CheckAndUploadAsync(file);
                    var fullPath = Path.Combine(hostingEnvironment.WebRootPath, filePath.TrimStart('/', '\\'));
                    return fullPath;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.Debug(ex);
                return null;
            }
        }
        public async Task<OperationResult<string>> UploadImage(IFormFile file, string urlImage, string applicationId, bool? isFront, bool dontUploadToIdv)
        {
            try
            {
                if (file != null || urlImage != null)
                {
                    var url = urlImage != null ? urlImage : await UploadImage(file);
                    if (url == null)
                    {
                        return null;
                    }
                    if (dontUploadToIdv == true)
                    {
                        return new OperationResult<string>
                        {
                            Succeed = true,
                            Data = url,
                            Message = OperationResult<string>.SuccessMessage
                        };
                    }
                    var client = new RestClient("https://services.idvpacific.com.au/api/Forms/File");
                    client.Timeout = -1;
                    var userName = configuration.GetSection("User_Name").Value;
                    var password = configuration.GetSection("Password").Value;
                    var key = "SIGN";
                    var title = "Signature";
                    var type = "0";

                    if (isFront == true)
                    {
                        key = "DLF";
                        title = "Driving Licence - Front";
                        type = "2";
                    }
                    else if (isFront == false)
                    {
                        key = "DLB";
                        title = "Driving Licence - Back";
                        type = "3";
                    }

                    var request = new RestRequest(Method.POST);
                    request.AddHeader("API-Username", userName);
                    request.AddHeader("API-Password", password);
                    request.AddHeader("Application_ID", applicationId);
                    request.AddHeader("Element_Key", key);
                    request.AddHeader("Element_Title", title);
                    request.AddHeader("ID_Document_Code", type);
                    request.AddHeader("OCR", "false");
                    request.AddFile("File", url);
                    IRestResponse response = client.Execute(request);
                    if (response.IsSuccessful)
                    {
                        var result = System.Text.Json.JsonSerializer.Deserialize<UploadFileViewModel>(response.Content);
                        if (!result.Message.Error)
                        {

                            return new OperationResult<string>
                            {
                                Succeed = true,
                                Data = url,
                                Message = OperationResult<string>.SuccessMessage
                            };
                        }
                    }
                    return new OperationResult<string>
                    {
                        Succeed = false,
                        Message = OperationResult<string>.ErrorMessage,
                    };
                };
                return new OperationResult<string> { Succeed = false, Message = "File Not Selected" };
            }
            catch (Exception ex)
            {
                logger.Debug(ex);
                return OperationResult<string>.Error(ex);
            }
        }
        public async Task<OperationResult<string>> CreateApplication()
        {
            try
            {
                var userName = configuration.GetSection("User_Name").Value;
                var password = configuration.GetSection("Password").Value;
                var client = new RestClient("https://services.idvpacific.com.au/api/Forms/Application");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("API-Username", userName);
                request.AddHeader("API-Password", password);
                request.AddParameter("Application_Title", "Nissan API Form");
                request.AddParameter("Date_Format", "yyyy-mm-dd");
                request.AddParameter("Branch_ID", "1b2cd180-7a57-11ec-ad77-7be29f4b9c78");


                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var result = System.Text.Json.JsonSerializer.Deserialize<CreateApplicationViewModel>(response.Content);

                    return new OperationResult<string>
                    {
                        Succeed = true,
                        Data = result.Result.Application_ID,

                        Message = OperationResult<int>.SuccessMessage
                    };
                }
                return new OperationResult<string>
                {
                    Succeed = false,
                    Message = OperationResult<int>.ErrorMessage,
                };
            }
            catch (Exception ex)
            {
                logger.Debug(ex);
                return OperationResult<string>.Error(ex);
            }
        }

        public async Task<OperationResult<string>> UpdateApplication(string applicationId, string firstname, string lastName, string email, string phoneNumber)
        {
            try
            {
                var userName = configuration.GetSection("User_Name").Value;
                var password = configuration.GetSection("Password").Value;
                var client = new RestClient("https://services.idvpacific.com.au/api/Forms/Application");
                client.Timeout = -1;
                var request = new RestRequest(Method.PUT);
                request.AddHeader("API-Username", userName);
                request.AddHeader("API-Password", password);


                request.AddParameter("Branch_ID", "1b2cd180-7a57-11ec-ad77-7be29f4b9c78");

                request.AlwaysMultipartFormData = true;
                request.AddParameter("Application_ID", applicationId);
                request.AddParameter("First_Name", firstname);
                request.AddParameter("Last_Name", lastName);
                request.AddParameter("Email_Address", email);
                request.AddParameter("Phone_Number", phoneNumber);

                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var result = System.Text.Json.JsonSerializer.Deserialize<CreateApplicationViewModel>(response.Content);

                    return new OperationResult<string>
                    {
                        Succeed = true,
                        Data = result.Result.Application_ID,

                        Message = OperationResult<int>.SuccessMessage
                    };
                }
                return new OperationResult<string>
                {
                    Succeed = false,
                    Message = OperationResult<int>.ErrorMessage,
                };
            }
            catch (Exception ex)
            {
                logger.Debug(ex);
                return OperationResult<string>.Error(ex);
            }
        }


        public async Task<OperationResult<FinalResultOCRDTO>> GetOCRResult(string frontUrl, string backUrl, string applicationId)
        {
            try
            {
                var IDVTask = _GetOCRResult(frontUrl, backUrl);
                var awsTask = _GetOCRResultByAmazonApi(frontUrl);
                await Task.WhenAll(IDVTask, awsTask);
                awsTask.Result.FrontUrl = frontUrl;
                awsTask.Result.BackUrl = backUrl;
                if (IDVTask.Result.Data != null)
                {
                    IDVTask.Result.Data.FrontUrl = frontUrl;
                    IDVTask.Result.Data.BackUrl = backUrl;
                    var faceUrl = _GetOCRImage(IDVTask.Result.Data.ImageFaseId, IDVTask.Result.Data.TransactionId);
                    var signatureUrl = _GetOCRImage(IDVTask.Result.Data.ImageSignatureId, IDVTask.Result.Data.TransactionId);
                    var frontImage = _GetOCRImage(IDVTask.Result.Data.ImageFrontId, IDVTask.Result.Data.TransactionId);
                    var backImage = IDVTask.Result.Data.ImageBackId.HasValue() ? _GetOCRImage(IDVTask.Result.Data.ImageBackId, IDVTask.Result.Data.TransactionId) : Task.FromResult<string>("");
                    await Task.WhenAll(faceUrl, signatureUrl, frontImage, backImage);
                    IDVTask.Result.Data.FaceUrl = faceUrl.Result;
                    IDVTask.Result.Data.SignatureUrl = signatureUrl.Result;
                    IDVTask.Result.Data.FrontUrl = frontImage.Result.HasValue() ? frontImage.Result : frontUrl;
                    IDVTask.Result.Data.BackUrl = backImage.Result.HasValue() ? backImage.Result :
                        backUrl;
                   
                    await Task.WhenAll(UploadImage(null, IDVTask.Result.Data.FrontUrl, applicationId, true, false), UploadImage(null, IDVTask.Result.Data.BackUrl, applicationId, false, false));

                    return new OperationResult<FinalResultOCRDTO> { Data = ComoareResult(IDVTask.Result.Data, awsTask.Result), Message = IDVTask.Result.Message, Succeed = IDVTask.Result.Succeed };
                }
                else
                {
                    await Task.WhenAll(UploadImage(null, backUrl, applicationId, false, false), UploadImage(null, frontUrl, applicationId, true, false));
                }

                //if (IDVTask.Result.Data.ImageFaseId!=null)
                //{
                //    IDVTask.Result.Data.ImageFaseId= await _GetOCRImage(IDVTask.Result.Data.ImageFaseId, IDVTask.Result.Data.TransactionId);
                //}




                return new OperationResult<FinalResultOCRDTO> { Data = ComoareResult(IDVTask.Result.Data, awsTask.Result), Message = "", Succeed = awsTask.Result != null };

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
                var userName = configuration.GetSection("User_Name").Value;
                var password = configuration.GetSection("Password").Value;
                var request = new RestRequest(Method.POST);
                request.AddHeader("API-Username", userName);
                request.AddHeader("API-Password", password);
                request.AddParameter("Date_Format", "yyyy-mm-dd");
                request.AddParameter("Processing_Type", "6");
                request.AddParameter("Document_Validation", "True");
                request.AddParameter("Overlay_Required", "True");
                request.AddParameter("Detect_Orientation", "True");
                request.AddParameter("Image_Scale", "False");
                request.AddParameter("Cropping_Mode", "True");
                request.AddFile("ID_Front_Image", fileFrontPath);
                if (fileBackPath == "||skip||" || fileBackPath == null ? false : true)
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
                            //MiddleName = result?.Result?.Data?.Middle_Name,
                            FirstName = result?.Result?.Data?.First_Name,
                            LastName = result?.Result?.Data?.Surname,
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
                            //MiddleName = x[2].value,
                            LastName = x[1].value,
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
            finalResultIdv = finalResultIdv == null ? new FinalResultOCRDTO { } : finalResultIdv;

            finalResultIdv.FirstName = !finalResultIdv.FirstName.HasValue() ? finalResultAZ?.FirstName : finalResultIdv?.FirstName;
            //finalResultIdv.MiddleName = !finalResultIdv.MiddleName.HasValue() ? finalResultAZ?.MiddleName : finalResultIdv?.MiddleName;
            finalResultIdv.LastName = !finalResultIdv.LastName.HasValue() ? finalResultAZ?.LastName : finalResultIdv?.LastName;
            finalResultIdv.FullName = !finalResultIdv.FullName.HasValue() ? finalResultAZ?.FullName : finalResultIdv?.FullName;
            finalResultIdv.Address = !finalResultIdv.Address.HasValue() ? finalResultAZ?.Address : finalResultIdv?.Address;
            finalResultIdv.DocumentNumber = !finalResultAZ.DocumentNumber.HasValue() ? finalResultIdv?.DocumentNumber : finalResultAZ.DocumentNumber;

            finalResultIdv.BirthDate = !finalResultAZ.BirthDate.HasValue() ? ConvertDate(finalResultIdv?.BirthDate) : ConvertDate(finalResultAZ?.BirthDate);
            finalResultIdv.ExpiryDate = !finalResultIdv.ExpiryDate.HasValue() ? ConvertDate(finalResultAZ?.ExpiryDate) : ConvertDate(finalResultIdv?.ExpiryDate);

            string ConvertDate(string date)
            {
                return date.HasValue() ? (date.Contains('/') || date.Contains('-') ? date.Substring(0, 10).Replace("-", "/") : Convert.ToDateTime(date.Replace("-", "/")).ToString("yyyy/MM/dd")) : "";
            }

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
                var userName = configuration.GetSection("User_Name").Value;
                var password = configuration.GetSection("Password").Value;
                var request = new RestRequest(Method.GET);
                request.AddHeader("API-Username", userName);
                request.AddHeader("API-Password", password);
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
        public async Task<OperationResult<object>> CreateElement(FinalResultOCRDTO result)
        {
            try
            {
                var client = new RestClient("https://services.idvpacific.com.au/api/Forms/Element");
                client.Timeout = -1;
                var userName = configuration.GetSection("User_Name").Value;
                var password = configuration.GetSection("Password").Value;

                var request = new RestRequest(Method.POST);
                request.AddHeader("API-Username", userName);
                request.AddHeader("API-Password", password);
                request.AddHeader("Application_ID", result.ApplicationId);
                var body = new
                {
                    Elements = new[]
                    {
                       new{Key="E1",Title="First_Name",Value=result.FirstName},
                       new{Key="E2",Title="Last_Name",Value=result.LastName},
                       new{Key="E3",Title="Full_Name",Value=result.FullName},
                       new{Key="E4",Title="Driving_Licence_Number",Value=result.DocumentNumber},
                       new{Key="E5",Title="Expiry_Date",Value=result.ExpiryDate},
                       new{Key="E6",Title="Birth_Date",Value=result.BirthDate},
                       new{Key="E7",Title="Address",Value=result.Address},
                       new{Key="E8",Title="T&C",Value="True"},
                   },
                };


                request.AddParameter("application/json", JsonConvert.SerializeObject(body), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var resultVM = System.Text.Json.JsonSerializer.Deserialize<CreateElementViewModel>(response.Content);
                    if (!resultVM.Message.Error)
                    {

                        return new OperationResult<object>
                        {
                            Succeed = true,
                            Message = OperationResult<object>.SuccessMessage
                        };
                    }
                }
                return new OperationResult<object>
                {
                    Succeed = false,
                    Message = OperationResult<object>.ErrorMessage,
                };
            }
            catch (Exception ex)
            {
                logger.Debug(ex);
                return OperationResult<object>.Error(ex);
            }
        }
        public async Task<OperationResult<object>> UploadSignature(FinalResultOCRDTO result)
        {
            try
            {
                var client = new RestClient("https://services.idvpacific.com.au/api/Forms/Element");
                client.Timeout = -1;
                var userName = configuration.GetSection("User_Name").Value;
                var password = configuration.GetSection("Password").Value;

                var request = new RestRequest(Method.POST);
                request.AddHeader("API-Username", userName);
                request.AddHeader("API-Password", password);
                request.AddHeader("Application_ID", result.ApplicationId);
                var body = new
                {
                    Elements = new[]
                    {
                       new{Key="E1",Title="First_Name",Value=result.FirstName},
                       new{Key="E2",Title="Last_Name",Value=result.LastName},
                       new{Key="E3",Title="Full_Name",Value=result.FullName},
                       new{Key="E4",Title="Driving_Licence_Number",Value=result.DocumentNumber},
                       new{Key="E5",Title="Expiry_Date",Value=result.ExpiryDate},
                       new{Key="E6",Title="Birth_Date",Value=result.BirthDate},
                       new{Key="E7",Title="Address",Value=result.Address},
                       new{Key="E8",Title="T&C",Value="True"},
                   },
                };


                request.AddParameter("application/json", JsonConvert.SerializeObject(body), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var resultVM = System.Text.Json.JsonSerializer.Deserialize<CreateElementViewModel>(response.Content);
                    if (!resultVM.Message.Error)
                    {

                        return new OperationResult<object>
                        {
                            Succeed = true,
                            Message = OperationResult<object>.SuccessMessage
                        };
                    }
                }
                return new OperationResult<object>
                {
                    Succeed = false,
                    Message = OperationResult<object>.ErrorMessage,
                };
            }
            catch (Exception ex)
            {
                logger.Debug(ex);
                return OperationResult<object>.Error(ex);
            }
        }
        public async Task<OperationResult<object>> ExecuteOcr(string appId)
        {
            try
            {
                var client = new RestClient("https://services.idvpacific.com.au/api/Forms/Execute");
                var request = new RestRequest(Method.POST);
                client.Timeout = -1;
                var userName = configuration.GetSection("User_Name").Value;
                var password = configuration.GetSection("Password").Value;
                var webhook = configuration.GetSection("Webhook").Value;
                request.AddHeader("API-Username", userName);
                request.AddHeader("API-Password", password);
                request.AddParameter("Application_ID", appId);
                request.AddParameter("Secure_Callback_URL", webhook);

                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var resultVM = System.Text.Json.JsonSerializer.Deserialize<CreateElementViewModel>(response.Content);
                    if (!resultVM.Message.Error)
                    {

                        return new OperationResult<object>
                        {
                            Succeed = true,
                            Message = OperationResult<object>.SuccessMessage
                        };
                    }
                }
                return new OperationResult<object>
                {
                    Succeed = false,
                    Message = OperationResult<object>.ErrorMessage,
                };
            }
            catch (Exception ex)
            {
                logger.Debug(ex);
                return OperationResult<object>.Error(ex);
            }
        }

    }
}