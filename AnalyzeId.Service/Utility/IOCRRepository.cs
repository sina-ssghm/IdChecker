using AnalyzeId.Domain.Model;

namespace AnalyzeId.Service.Utility
{
    public interface IOCRRepository
    {
        void Add(FinalResultOCRDTO final);
    }
}