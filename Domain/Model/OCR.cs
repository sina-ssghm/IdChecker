
using AnalyzeId.Domain.ViewModel;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeId.Domain.Model
{
    public class OCR: BaseViewModel
    {
   
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
        public string DocumentNumber { get; set; }
        public string BirthDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Address { get; set; }
        public string TransactionId { get; set; }

        public OCRFileViewModel  OCRFile{ get; set; }
    }
}
