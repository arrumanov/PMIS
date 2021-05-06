using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dogovor.CrossCutting.Extensions.GraphQL;
using Dogovor.CrossCutting.Interfaces;

namespace Dogovor.Infrastructure.Database.Query
{
    public interface IManager<T> where T : class, IQueryModel
    {
        Task<bool> Index(T entry);
        Task<bool> Remove(Guid entryId);
        Task<T> GetById(Guid id, string[] fields);
        Task<IEnumerable<T>> Get(string[] fields, IDictionary<string, GraphFilter> filters, string order, int skip, int take);
    }
}