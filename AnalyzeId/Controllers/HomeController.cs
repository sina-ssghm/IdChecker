
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
        private readonly IServiceScopeFactory serviceProvider;
        private readonly IImagePassportUrlRepository passportUrlRepository;
        private readonly IOCRService service;

        public HomeController(IOCRService oCRService, IOCRRepository oCRRepository, IOCRFilesRepository oCRFilesRepository, IServiceScopeFactory serviceProvider, IImagePassportUrlRepository passportUrlRepository)
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

        public async Task<IActionResult> OcrRequest()
        {
            var appId = await oCRService.CreateApplication();
            return View(new OCRFileDTO { ApplicationId = appId.Data });
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
            if (fileDTO.UrlBack != "||skip||")
            {
                var isfront = fileDTO.UrlFront != null ? false : true;
                var res = await oCRService.UploadImage(fileDTO.File, null, fileDTO.ApplicationId, isfront,dontUploadToIdv:true);
                if (res.Succeed && isfront)
                {
                    fileDTO.UrlFront = res.Data;
                }
                else
                {
                    fileDTO.UrlBack = res.Data;
                }
            }


            //var idPass = fileDTO?.IdPass != null ? fileDTO.IdPass.Value : passportUrlRepository.Add(new ImagePassportViewModel { }).GetAwaiter().GetResult().Data;
            //if (fileDTO.UrlBack == "||skip||" && fileDTO.IsUploadFront)
            //{
            //    await passportUrlRepository.Update(fileDTO.IdPass.Value, fileDTO.UrlBack, false);
            //}
            //UploadImage(fileDTO.File, idPass, fileDTO?.IdPass != null ? false : true);

            if (fileDTO.UrlFront != null && (fileDTO.UrlBack == "||skip||" || fileDTO.UrlBack != null))
            {
                fileDTO.UrlBack = fileDTO.UrlBack == "||skip||" ? null : fileDTO.UrlBack;
                fileDTO.Succeed = true;
                fileDTO.IsContinue = true;
                return View(fileDTO);
            }
            //fileDTO.IdPass = idPass;
            return View(nameof(OcrRequest), fileDTO);
        }

        public async Task<IActionResult> GetOCRResult(OCRFileDTO fileDTO)
        {
            if (fileDTO?.UrlFront == null || fileDTO?.ApplicationId == null)
            {
                return NotFound();
            }
            var result = await oCRService.GetOCRResult(fileDTO.UrlFront, fileDTO.UrlBack, fileDTO?.ApplicationId);
            result.Data.ApplicationId = fileDTO.ApplicationId;
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmResult(FinalResultOCRDTO model)
        {
            var res = oCRService.CreateElement(model);
            var updateApp = oCRService.UpdateApplication(model.ApplicationId, model.FirstName, model.LastName, model.Email, model.PhoneNumber);
            await Task.WhenAll(res, updateApp);

            if (res.Result.Succeed)
            {
                return RedirectToAction(nameof(Signature), new { appId = model.ApplicationId });
            }
            return View(new OperationResult<FinalResultOCRDTO> { Message = "Error", Succeed = false });

        }

        [HttpGet]
        public async Task<IActionResult> Signature(string appId)
        {
            return View(new SignatureViewModel { ApplicationId = appId });
        }

        [HttpPost]
        public async Task<IActionResult> Signature(string file, string type, string applicationId)
        {
            try
            {
                var url = oCRService.SaveImageBase64(file, type);
                var res = await oCRService.UploadImage(null, url, applicationId, null, dontUploadToIdv: false);
                if (res.Succeed)
                {
                    return Json("true");
                }
                return Json(res.Message);

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
                throw;
            }
        }

        public async Task<IActionResult> ThankYou(string appId)
        {
            await oCRService.ExecuteOcr(appId);
            return View(nameof(ThankYou));
        }


        public async Task UploadImage(IFormFile file, Guid id, bool isFront)
        {
            var service = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IOCRService>();
            await service.UploadImage(file, id, isFront);
        }
    }
}
