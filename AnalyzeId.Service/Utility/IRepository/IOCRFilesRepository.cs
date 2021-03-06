using AnalyzeId.Domain.Model;
using AnalyzeId.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace AnalyzeId.Service.Utility
{
    public interface IOCRFilesRepository
    {
        void Add(FinalResultOCRDTO model);
        List<OCRFileViewModel> GetAll(string transactionId);
        List<OCRFileForApiViewModel> GetAllForOcrApi(string transactionId);
        Byte[] GetImage(string transactionId, Guid imageId);
        void _Add(OCRFileViewModel model);
    }
}