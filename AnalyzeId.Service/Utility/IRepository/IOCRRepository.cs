using AnalyzeId.Domain.Model;
using AnalyzeId.Domain.ViewModel;
using System.Collections.Generic;

namespace AnalyzeId.Service.Utility
{
    public interface IOCRRepository
    {
        void Add(FinalResultOCRDTO final);
        List<OCR> Get(string transactionId);
        List<OCRForApiViewModel> GetForApi(string transactionId);
    }
}