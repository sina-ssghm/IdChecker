using AnalyzeId.Domain.ViewModel;
using AnalyzeId.Service.Utility;
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

        public IOCRRepository OCRRepository { get; }

        public OCRController(IOCRRepository oCRRepository,IOCRFilesRepository oCRFilesRepository)
        {
            OCRRepository = oCRRepository;
            this.oCRFilesRepository = oCRFilesRepository;
        }

        // GET: api/<OCRController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<OCRController>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            var data = new ResultOCRAndFilesViewModel
            {
                OCRs = OCRRepository.Get(id),
                OCRFiles = oCRFilesRepository.GetAll(id)
            };
            return JsonConvert.SerializeObject(data);
        }

        [HttpGet("{id}/{imgId}")]
        public string GetFile(string id, Guid imgId)
        {
            var result = oCRFilesRepository.GetImage(id,imgId);
            return JsonConvert.SerializeObject(result);
        }

        // POST api/<OCRController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OCRController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OCRController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
