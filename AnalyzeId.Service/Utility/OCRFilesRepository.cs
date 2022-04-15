using AnalyzeId.Domain.Enum;
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
    public class OCRFilesRepository : IOCRFilesRepository
    {
        private readonly IMongoDatabase db;
        private readonly IMongoCollection<OCRFile> collection;

        public OCRFilesRepository()
        {
            var client = new MongoClient();
            db = client.GetDatabase("IdChecker");
            collection = db.GetCollection<OCRFile>("OCRFile");
        }

        public void _Add(OCRFileViewModel model)
        {
            collection.InsertOne(new OCRFile
            {
                File = model.File,
                FileType = model.FileType,
                TransactionId = model.TransactionId,
                UniqueId = model.UniqueId
            });
        }  
        public void Add(FinalResultOCRDTO model)
        {
            if (model.FrontUrl != null)
            {
                _Add(new OCRFileViewModel
                {
                    File = model.FrontUrl.UrlToDirectoryPath(),
                    FileType = FileType.Front,
                    TransactionId = model.TransactionId,
                    UniqueId = model.ImageFrontId
                });
            }
            if (model.BackUrl != null)
            {
                _Add(new OCRFileViewModel
                {
                    File = model.BackUrl.UrlToDirectoryPath(),
                    FileType = FileType.Back,
                    TransactionId = model.TransactionId,
                    UniqueId = model.ImageBackId
                });
            }
            if (model.FaceUrl != null)
            {

                _Add(new OCRFileViewModel
                {
                    File = model.FaceUrl.UrlToDirectoryPath(),
                    FileType = FileType.Face,
                    TransactionId = model.TransactionId,
                    UniqueId = model.ImageFaseId
                });
            }
            if (model.SignatureUrl != null)
            {

                _Add(new OCRFileViewModel
                {
                    File = model.SignatureUrl.UrlToDirectoryPath(),
                    FileType = FileType.ApiSignoture,
                    TransactionId = model.TransactionId,
                    UniqueId = model.ImageSignatureId
                });
            }
        }

    }
}
