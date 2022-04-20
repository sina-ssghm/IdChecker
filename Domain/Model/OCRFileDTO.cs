using Microsoft.AspNetCore.Http;
using System;

namespace AnalyzeId.Domain.Model
{
    public class OCRFileDTO
    {
        public IFormFile  File{ get; set; }

        public string UrlFront { get; set; }
        public string UrlBack { get; set; }
        public string Message { get; set; }
        public bool IsContinue { get; set; }
        public bool Succeed { get; set; }
        public bool IsShowResult { get; set; }
        public Guid? IdPass { get; set; }
        public string ApplicationId { get; set; }
    }
}
