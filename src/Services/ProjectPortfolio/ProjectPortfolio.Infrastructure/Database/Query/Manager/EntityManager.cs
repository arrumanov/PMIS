using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.Infrastructure.Database.Query.Manager
{
    public abstract class EntityManager<T> : IEntityManager<T> where T : class, IQueryModel
    {
        protected readonly IManager<T> _Manager;

        public EntityManager(IManager<T> manager)
        {
            _Manager = manager;
        }

        public Task<IEnumerable<T>> Get(string[] fields, IDictionary<string, GraphFilter> filters, string order, int skip, int take)
        {
            return _Manager.Get(fields, filters, order, skip, take);
        }

        public Task<IQueryable<T>> Get()
        {
            return _Manager.Get();
        }

        public Task<T> GetById(Guid id, string[] fields)
        {
            return _Manager.GetById(id, fields);
        }

        public Task<T> GetById(Guid id)
        {
            return _Manager.GetById(id);
        }

        public Task<bool> Index(T entry)
        {
            return _Manager.Index(entry);
        }

        public Task<bool> Remove(Guid entryId)
        {
            return _Manager.Remove(entryId);
        }
    }
}