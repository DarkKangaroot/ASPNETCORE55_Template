using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface _IGenericsRepository<T> where T : class
    {
        Task<IEnumerable<T>> Query(object param, string queryText, bool isSp = false);
        Task<T> QuerySingle(object param, string queryText, bool isSp = false);
        Task Execute(object param, string queryText, bool isSp = false);
    }
}
