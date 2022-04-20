using AnalyzeId.Domain.ViewModel;
using AnalyzeId.Service;
using AnalyzeId.Service.Utility;
using AnalyzeId.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AnalyzeId.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OCRController : ControllerBase
    {
        private readonly IOCRFilesRepository oCRFilesRepository;
        private readonly AccountService accountService;

        public IOCRRepository OCRRepository { get; }

        public OCRController(IOCRRepository oCRRepository, IOCRFilesRepository oCRFilesRepository, AccountService accountService)
        {
            OCRRepository = oCRRepository;
            this.oCRFilesRepository = oCRFilesRepository;
            this.accountService = accountService;
        }

        // GET api/<OCRController>/5
        [HttpGet("{id}")]
        public string Get(string transactionId, [FromHeader(Name = "API-UserName")] string username, [FromHeader(Name = "API-Password")] string pass)
        {
            var operation = new OperationResult<object> { Succeed = true, Message = "Done Successfully" };

            if (transactionId==null)
            {
                operation.Message = "Transaction-ID not valid";
                operation.Succeed = false;
                return JsonConvert.SerializeObject(operation);
            }
            var res = accountService.IsValidUser(username, pass).GetAwaiter().GetResult();
         
            if (!res)
            {
                operation.Message = "User is not valid";
                operation.Succeed = false;
                return JsonConvert.SerializeObject(operation);
            }
            var data = new ResultOCRAndFilesViewModel
            {
                Result = OCRRepository.GetForApi(transactionId),
                 Message =new Message
                 {
                      Description =operation.Message,
                       Error =operation.Succeed,
                 },
                Transaction=new Transaction
                {
                      Transaction_Id = transactionId,
                       Username = username,
                    Name = "Ocr get information",
                     
                }
            };
            
            return JsonConvert.SerializeObject(data);
        }

        [HttpGet]
        [Route("/Api/GetFile")]
        public string GetFile(string transactionId, Guid imageId, [FromHeader(Name = "API-UserName")] string username, [FromHeader(Name = "API-Password")] string pass)
        {

            var operation = new GetImageBase64ForApiViewModel { Succeed=true, Message= "Done Successfully" };
            if (transactionId == null)
            {
                operation.Message = "Transaction-ID not valid";
                operation.Succeed = false;
                return JsonConvert.SerializeObject(operation);
            }
            var res = accountService.IsValidUser(username, pass).GetAwaiter().GetResult();
            if (!res)
            {
                operation.Message = "User is not valid";
            }
            var result = oCRFilesRepository.GetImage(transactionId, imageId);
            operation.Data = result;
            return JsonConvert.SerializeObject(operation);
        }

    


    }
}
