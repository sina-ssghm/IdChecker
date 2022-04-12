using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnalyzeId.Shared
{
    public interface IFileUploader
    {
        Task<string> CheckAndUploadAsync(IFormFile file);
        Task<string> CheckAndUploadBase64Async(string base64);
        void DeleteFiles(List<string> files);
        Task<List<string>> UploadFiles(List<IFormFile> files);
    }
}