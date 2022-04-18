using AnalyzeId.Domain.Model;
using AnalyzeId.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace AnalyzeId.Service.Utility
{
    public interface IOCRService
    {
        Task<OperationResult<FinalResultOCRDTO>> GetOCRResult(Guid Id);
        string SaveImageBase64(string base64, string type);
        void SendRequestToWebhook(string transactionId);
        Task<OperationResult<FileUploadPathDTO>> UploadImage(string file);
        Task<OperationResult<FileUploadPathDTO>> UploadImage(IFormFile file, Guid id, bool isFront);
    }
}