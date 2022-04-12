using AnalyzeId.Shared;
using AnalyzeId.Shared.DTO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AnalyzeId.Service.Utility
{
    public interface IOCRService
    {
        Task<OperationResult<FinalResultOCRDTO>> GetOCRResult(string fileFrontPath, string fileBackPath);
        Task<OperationResult<FileUploadPathDTO>> UploadImage(string file);
        Task<OperationResult<FileUploadPathDTO>> UploadImage(IFormFile file);
    }
}