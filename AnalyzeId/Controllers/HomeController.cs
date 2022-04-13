using AnalyzeId.Shared.DTO;
using AnalyzeId.Service.Utility;
using AnalyzeId.Shared;
using AnalyzeId.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.IO;
using Amazon.Textract;
using Amazon.Textract.Model;

namespace AnalyzeId.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOCRService oCRService;

        public HomeController(IOCRService oCRService)
        {
            this.oCRService = oCRService;
        }
        public async Task<IActionResult> Index(string frontPath)
        {
            return View(new OCRFileDTO { UrlFront = frontPath });
        }

        public async Task<IActionResult> OcrRequest(string frontPath)
        {
            return View(new OCRFileDTO { UrlFront = frontPath });
        }

        public async Task<IActionResult> CanvasResult(string file)
        {
            var result = await oCRService.UploadImage(file);
            return View(nameof(Result), new OCRFileDTO
            {
                UrlFront = result?.Data?.FullPath,
                //UrlBack=,
                Succeed = result.Succeed,
            });
        }

        public async Task<IActionResult> Result(OCRFileDTO fileDTO)
        {
            var result = await oCRService.UploadImage(fileDTO.File);
            if ((fileDTO.File == null && fileDTO.UrlFront != null) || (fileDTO.File != null && fileDTO.UrlFront != null))
            {
                fileDTO.UrlFront = fileDTO.UrlFront ?? result.Data.FullPath;
                fileDTO.UrlBack = fileDTO.UrlFront == null ? null : result?.Data?.FullPath;
                fileDTO.Succeed = true;
                fileDTO.IsContinue = true;
                return View(fileDTO);
            }
            fileDTO.UrlFront = fileDTO.UrlFront ?? result.Data.FullPath;
            return View(nameof(OcrRequest), fileDTO);
        }

        public async Task<IActionResult> GetOCRResult(OCRFileDTO fileDTO)
        {
            var result = await oCRService.GetOCRResult(fileDTO.UrlFront, fileDTO.UrlBack);
            return View(result);
        }

        public async Task<IActionResult> ConfirmResult()
        {
            return RedirectToAction(nameof(Signature));
        }
        [HttpGet]
        public async Task<IActionResult> Signature()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signature(int c)
        {
            return View();
        }

        public async Task<IActionResult> ThankYou()
        {
            return View();
        }
    }
}
