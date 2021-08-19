using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dogovor.CrossCutting.Extensions.GraphQL;

namespace Dogovor.Infrastructure.Database.Command.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        Task Add(T obj);
        Task<T> GetById(Guid id);
        Task<IQueryable<T>> GetAll();
        Task<IQueryable<T>> Get(IDictionary<string, GraphFilter> filters);
        Task Update(T obj);
        Task Remove(T obj);
    }
}