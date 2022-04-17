using AnalyzeId.Domain.ViewModel;
using AnalyzeId.Shared;
using System.Threading.Tasks;

namespace AnalyzeId.Service.Utility
{
    public interface IImagePassportUrlRepository
    {
        Task<OperationResult<object>> Add(ImagePassportViewModel final);
        Task<ImagePassportViewModel> Get(string id);
    }
}