using AnalyzeId.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnalyzeId.Service.Utility
{
    public interface IUnitOfWork<TCollection> where TCollection : class
    {
        Task<OperationResult<object>> AddAsync(TCollection collection2);
        Task<IEnumerable<TCollection>> GetAllAsync();
        Task<IEnumerable<TCollection>> GetManyAsync(IEnumerable<string> ids);
        Task<TCollection> GetOneAsync(string id);
        Task<OperationResult<object>> RemoveManyAsync(IEnumerable<string> ids);
        Task<OperationResult<object>> RemoveOneAsync(string id);
    }
}