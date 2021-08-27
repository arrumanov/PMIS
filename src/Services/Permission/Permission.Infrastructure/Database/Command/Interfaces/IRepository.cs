using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Permission.CrossCutting.Extensions.GraphQL;

namespace Permission.Infrastructure.Database.Command.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        Task Add(T obj);
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetByIds(List<Guid> ids);
        Task<IQueryable<T>> GetAll();
        Task<IQueryable<T>> Get(IDictionary<string, GraphFilter> filters);
        Task Update(T obj);
        Task Remove(T obj);
    }
}