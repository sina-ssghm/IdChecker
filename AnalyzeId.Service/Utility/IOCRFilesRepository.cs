using AnalyzeId.Domain.Model;
using AnalyzeId.Domain.ViewModel;

namespace AnalyzeId.Service.Utility
{
    public interface IOCRFilesRepository
    {
        void Add(FinalResultOCRDTO model);
        void _Add(OCRFileViewModel model);
    }
}