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

        public OCRRepository()
        {
            var client = new MongoClient();
            db = client.GetDatabase("IdChecker");
            collection = db.GetCollection<OCR>("OCR");
        }

        public void Add(FinalResultOCRDTO final)
        {
            collection.InsertOne(new OCR
            {
                Address = final?.Address,
                FirstName = final?.FirstName,
                Surname = final?.Surname,
                BirthDate = final?.BirthDate,
                DocumentNumber = final?.DocumentNumber,
                ExpiryDate = final?.ExpiryDate,
                FullName = final?.FullName,
                MiddleName = final?.MiddleName,
                TransactionId=final?.TransactionId,
            });
        }


        public List<OCR> Get(string transactionId)
        {
          var result=  collection.Find(s=>s.TransactionId==transactionId).ToList();
            return result;
        }
        public List<OCRForApiViewModel> GetForApi(string transactionId)
        {
            return collection.Find(s => s.TransactionId == transactionId).ToList()
                .Select(p => new OCRForApiViewModel
                {
                    Address = p.Address,
                    BirthDate = p.BirthDate,
                    DocumentNumber = p.DocumentNumber,
                    ExpiryDate = p.ExpiryDate,
                    FirstName = p.FirstName,
                    FullName = p.FullName,
                    MiddleName = p.MiddleName,
                    Surname = p.Surname,
                    TransactionId = p.TransactionId,
                }).ToList();
        }

    }
}
