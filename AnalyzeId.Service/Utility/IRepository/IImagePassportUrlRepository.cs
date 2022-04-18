using AnalyzeId.Domain.ViewModel;
using AnalyzeId.Shared;
using System;
using System.Threading.Tasks;

namespace AnalyzeId.Service.Utility
{
    public interface IImagePassportUrlRepository
    {
        Task<OperationResult<Guid>> Add(ImagePassportViewModel final);
        Task<ImagePassportViewModel> Get(Guid id);
        Task<OperationResult<Guid>> Update(Guid id, string img, bool isFront);
    }
}