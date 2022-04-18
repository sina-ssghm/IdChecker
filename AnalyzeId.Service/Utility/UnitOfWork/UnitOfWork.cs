using AnalyzeId.Domain.Enum;
using AnalyzeId.Domain.Model;
using AnalyzeId.Domain.ViewModel;
using AnalyzeId.Shared;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeId.Service.Utility
{
    public class UnitOfWork<TCollection> : IUnitOfWork<TCollection> where TCollection : class
    {
        private readonly IMongoDatabase db;
        private readonly IMongoCollection<TCollection> collection;

        public UnitOfWork()
        {
            var client = new MongoClient();
            db = client.GetDatabase("IdChecker");
            collection = db.GetCollection<TCollection>("OCRFile");
     

        }



        public async Task<OperationResult<object>> AddAsync(TCollection collection2)
        {
            try
            {
                await collection.InsertOneAsync(collection2);

                return OperationResult<object>.Complete;
            }
            catch (Exception ex)
            {
                return OperationResult<object>.Error(ex);
                throw;
            }
        }


        public async Task<IEnumerable<TCollection>> GetAllAsync()
        {
            return await collection.Find(f => true).ToListAsync();
        }

        public async Task<TCollection> GetOneAsync(string id)
        {
            return await collection.Find(new BsonDocument("_id", new Guid(id))).FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<TCollection>> GetManyAsync(IEnumerable<string> ids)
        {
            var list = new List<TCollection>();
            foreach (var id in ids)
            {
                var doc = await GetOneAsync(id);
                if (doc == null) continue;
                list.Add(doc);
            }

            return list;
        }


        public async Task<OperationResult<object>> RemoveOneAsync(string id)
        {
            try
            {
                await collection.DeleteOneAsync(
             new BsonDocument("_id", new Guid(id)));
                return OperationResult<object>.Complete;
            }
            catch (Exception ex)
            {
                return OperationResult<object>.Error(ex);
                throw;
            }
        }

        public async Task<OperationResult<object>> RemoveManyAsync(IEnumerable<string> ids)
        {
            try
            {
                foreach (var id in ids)
                    await RemoveOneAsync(id);
                return OperationResult<object>.Complete;
            }
            catch (Exception ex)
            {
                return OperationResult<object>.Error(ex);
                throw;
            }
        }

    }
}
