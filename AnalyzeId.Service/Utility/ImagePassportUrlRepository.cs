using AnalyzeId.Domain.Model;
using AnalyzeId.Domain.ViewModel;
using AnalyzeId.Shared;
using MongoDB.Bson;
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
        private readonly IMongoDatabase db;
        private readonly IMongoCollection<ImagePassport> collection;

        public ImagePassportUrlRepository()
        {
            var client = new MongoClient();
            db = client.GetDatabase("IdChecker");
            collection = db.GetCollection<ImagePassport>("ImagePassport");
        }

    
        public async Task<OperationResult<Guid>> Add(ImagePassportViewModel final)
        {
            var imagePassport = new ImagePassport
            {
                BackUrl = final.BackUrl,
                FrontUrl = final.FrontUrl,
            };
             await collection.InsertOneAsync(imagePassport);
            return new OperationResult<Guid> { Succeed=true,Data= imagePassport.Id };
        }
        public async Task<ImagePassportViewModel> Get(Guid id)
        {
            var res = await collection.Find(s => s.Id == id).FirstOrDefaultAsync();
            return new ImagePassportViewModel
            {
                Id = res.Id,
                BackUrl = res.BackUrl,
                FrontUrl = res.FrontUrl,
                 
            };
        }    
        public async Task<OperationResult<Guid>> Update(Guid id,string img,bool isFront)
        {
            var res =  collection.Find(s => s.Id == id).FirstOrDefault();
            if (res==null)
            {
                return null;
            }
            res.FrontUrl = isFront? img:res.FrontUrl;
            res.BackUrl = isFront? "":img;
            var filter = Builders<ImagePassport>.Filter.Eq(s => s.Id, id);
            var result = await collection.ReplaceOneAsync(filter, res);
            return new OperationResult<Guid> { Succeed = true, Data = res.Id };
        }

       
    }
}
