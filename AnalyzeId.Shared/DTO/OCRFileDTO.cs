using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeId.Shared.DTO
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
    }
}
