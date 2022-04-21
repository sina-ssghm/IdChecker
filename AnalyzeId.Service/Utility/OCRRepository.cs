using AnalyzeId.Domain.Model;
using AnalyzeId.Domain.ViewModel;
using AnalyzeId.Shared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeId.Service.Utility
{
    public class OCRRepository : IOCRRepository
    {
        private readonly IMongoDatabase db;
        private readonly IMongoCollection<OCR> collection;
        private readonly IOCRFilesRepository oCRFilesRepository;

        public OCRRepository(IOCRFilesRepository oCRFilesRepository)
        {
            var client = new MongoClient();
            db = client.GetDatabase("IdChecker");
            collection = db.GetCollection<OCR>("OCR");
            this.oCRFilesRepository = oCRFilesRepository;
        }

        public void Add(FinalResultOCRDTO final)
        {
            collection.InsertOne(new OCR
            {
                Address = final?.Address,
                FirstName = final?.FirstName,
                Surname = final?.LastName,
                BirthDate = final?.BirthDate,
                DocumentNumber = final?.DocumentNumber,
                ExpiryDate = final?.ExpiryDate,
                FullName = final?.FullName,
                //MiddleName = final?.MiddleName,
                TransactionId=final?.TransactionId,
                JsonResponse=final?.JsonResultIDv,
            });
        }


        public List<OCR> Get(string transactionId)
        {
          var result=  collection.Find(s=>s.TransactionId==transactionId).ToList();
            return result;
        }
        public Domain.ViewModel.Result GetForApi(string transactionId)
        {
            var data= collection.Find(s => s.TransactionId == transactionId).FirstOrDefault();
            var result = new OCRDTO { };
            if (!string.IsNullOrEmpty(data?.JsonResponse))
            {
                result = System.Text.Json.JsonSerializer.Deserialize<OCRDTO>(data?.JsonResponse);
            }
            return new Domain.ViewModel.Result
            {

                 Elements=new Elements
                 {
                     List=new Element_List
                     {
                          Element_1=new Element {  Title= "First_Name", Value=data.FirstName },
                          Element_2=new Element { Title = "Middle_Name", Value = data.MiddleName },
                         Element_3 =new Element { Title = "Surname", Value = data.Surname },
                         Element_4 =new Element { Title = "Full_Name", Value = data.FullName },
                         Element_5 =new Element { Title = "Document_Number", Value = data.DocumentNumber },
                         Element_6 =new Element { Title = "Birth_Date", Value = data.BirthDate },
                         Element_7 =new Element { Title = "Expiry_Date", Value = data.ExpiryDate },
                         Element_8 =new Element { Title = "Address", Value = data.Address },
                         Element_9 =new Element { Title = "Transaction_ID", Value = data.TransactionId },                                           
                     }
                 },
                Classification = result?.Result?.Data,
                 OCRFile = oCRFilesRepository.GetAllForOcrApi(transactionId),
            };
        }

    }
}
