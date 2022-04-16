using AnalyzeId.Domain.Model;
using System.Collections.Generic;

namespace AnalyzeId.Service.Utility
{
    public interface IOCRRepository
    {
        void Add(FinalResultOCRDTO final);
        List<OCR> Get(string transactionId);
    }
}