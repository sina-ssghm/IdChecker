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
    public class ImagePassportUrlRepository : IImagePassportUrlRepository
    {
        private readonly UnitOfWork<ImagePassport> unitOfWork;

        public ImagePassportUrlRepository(UnitOfWork<ImagePassport> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<object>> Add(ImagePassportViewModel final)
        {
            return await unitOfWork.AddAsync(new ImagePassport
            {
                BackUrl = final.BackUrl,
                FrontUrl = final.FrontUrl,
            });

        }
        public async Task<ImagePassportViewModel> Get(string id)
        {
            var res = await unitOfWork.GetOneAsync(id);
            return new ImagePassportViewModel
            {
                Id = res.Id,
                BackUrl = res.BackUrl,
                FrontUrl = res.FrontUrl,
            };
        }

    }
}
