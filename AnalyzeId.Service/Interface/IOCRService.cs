using Amazon.Rekognition.Model;
using AnalyzeId.Domain.Model;
using AnalyzeId.Domain.ViewModel.UploadFile;
using AnalyzeId.Shared;
using Microsoft.AspNetCore.Http;
using PassportTextExtract.Models;
using System;
using System.Threading.Tasks;

namespace AnalyzeId.Service.Utility
{
    public interface IOCRService
    {
        Task<OperationResult<string>> CreateApplication();
        Task<OperationResult<object>> CreateElement(FinalResultOCRDTO result);
        Task<OperationResult<object>> ExecuteOcr(string appId);
        Task<CompareFacesResponse> FaceCompare(string doc, string selfie);
        Task<OperationResult<FinalResultOCRDTO>> GetOCRResult(string frontUrl, string backUrl, string selfieUrl, string applicationId);
        Task<LivenessResult> Liveness(string path);
        string SaveImageBase64(string base64, string type);
        void SendRequestToWebhook(string transactionId);
        Task<OperationResult<string>> UpdateApplication(string applicationId, string firstname, string lastName, string email, string phoneNumber);
        Task<OperationResult<FileUploadPathDTO>> UploadImage(string file);
        Task<OperationResult<FileUploadPathDTO>> UploadImage(IFormFile file, Guid id, bool isFront);
        Task<OperationResult<string>> UploadImage(IFormFile file, string urlImage, string applicationId, bool? isFront,bool isSelfie, bool dontUploadToIdv);
    }
}