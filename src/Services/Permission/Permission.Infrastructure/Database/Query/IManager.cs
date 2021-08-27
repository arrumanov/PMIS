using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Permission.CrossCutting.Extensions.GraphQL;
using Permission.CrossCutting.Interfaces;

namespace Permission.Infrastructure.Database.Query
{
    public interface IManager<T> where T : class, IQueryModel
    {
        Task<bool> Index(T entry);
        //Task<bool> IndexAll(List<T> entry);
        Task<bool> Remove(Guid entryId);
        Task<T> GetById(Guid id, string[] fields);
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> Get(string[] fields, IDictionary<string, GraphFilter> filters, string order, int skip, int take);
        Task<IQueryable<T>> Get();
    }
}