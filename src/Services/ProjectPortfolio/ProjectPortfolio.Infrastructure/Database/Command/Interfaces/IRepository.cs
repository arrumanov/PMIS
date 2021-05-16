using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPortfolio.Infrastructure.Database.Command.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        Task Add(T obj);
        Task<T> GetById(Guid id);
        Task<IQueryable<T>> GetAll();
        Task Update(T obj);
        Task Remove(T obj);
    }
}