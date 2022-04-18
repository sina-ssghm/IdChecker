
using AnalyzeId.Service.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnalyzeId.Domain.Model;
using AnalyzeId.Domain.ViewModel;
using AnalyzeId.Domain.Enum;
using AnalyzeId.Shared;
using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace AnalyzeId.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOCRService oCRService;
        private readonly IOCRRepository oCRRepository;
        private readonly IOCRFilesRepository oCRFilesRepository;
        private readonly IServiceProvider serviceProvider;
        private readonly IImagePassportUrlRepository passportUrlRepository;

        public HomeController(IOCRService oCRService, IOCRRepository oCRRepository, IOCRFilesRepository oCRFilesRepository, IServiceProvider  serviceProvider,IImagePassportUrlRepository passportUrlRepository)
        {
            this.oCRService = oCRService;
            this.oCRRepository = oCRRepository;
            this.oCRFilesRepository = oCRFilesRepository;
            this.serviceProvider = serviceProvider;
            this.passportUrlRepository = passportUrlRepository;
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
            var isbackImage = fileDTO.IdPass.HasValue;
           var idPass=fileDTO?.IdPass!=null?fileDTO.IdPass.Value: passportUrlRepository.Add(new ImagePassportViewModel { }).GetAwaiter().GetResult().Data;
            var serviceOcr = serviceProvider.GetRequiredService<IOCRService>();
            var result = serviceOcr.UploadImage(fileDTO.File, idPass,fileDTO?.IdPass!=null?false:true);

            if (isbackImage)
            {
                //fileDTO.UrlFront = fileDTO.UrlFront ?? result.Data.FullPath;
                //fileDTO.UrlBack = fileDTO.UrlFront == null ? null : result?.Data?.FullPath;
                fileDTO.Succeed = true;
                fileDTO.IsContinue = true;
                fileDTO.IdPass = idPass;
                return View(fileDTO);
            }
            fileDTO.IdPass = idPass;
            return View(nameof(OcrRequest), fileDTO);
        }

        public async Task<IActionResult> GetOCRResult(OCRFileDTO fileDTO)
        {
            if (fileDTO?.IdPass == null)
            {
                return NotFound();
            }
            var result = await oCRService.GetOCRResult(fileDTO.IdPass.Value);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmResult(FinalResultOCRDTO model)
        {
            oCRRepository.Add(model);
            oCRFilesRepository.Add(model);
            return RedirectToAction(nameof(Signature),new { transactionId =model.TransactionId});
        }

        [HttpGet]
        public async Task<IActionResult> Signature(string transactionId)
        {
            return View(new SignatureViewModel {TransactionId=transactionId });
        }

        [HttpPost]
        public async Task<IActionResult> Signature(string file,string type,string transactionId)
        {
            try
            {
                var url = oCRService.SaveImageBase64(file, type);
                if (url.HasValue())
                {
                    oCRFilesRepository._Add(new OCRFileViewModel
                    {
                        File = url.UrlToDirectoryPath(),
                        FileType = FileType.Signoture,
                        TransactionId = transactionId,
                        UniqueId = null,
                    });
                }
                if (transactionId!=null)
                {
                    oCRService.SendRequestToWebhook(transactionId);
                }
                return Json("true");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
                throw;
            }
        }

        public async Task<IActionResult> ThankYou()
        {
            return View();
        }
    }
}
