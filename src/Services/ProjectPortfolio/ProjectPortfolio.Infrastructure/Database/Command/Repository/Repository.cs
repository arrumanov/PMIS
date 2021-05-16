﻿using System;
using System.Linq;
using System.Threading.Tasks;
using ProjectPortfolio.CrossCutting.Extensions;
using ProjectPortfolio.Infrastructure.Database.Command.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ProjectPortfolio.Infrastructure.Database.Command.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected GraphContext _Context;

        public Repository(GraphContext context)
        {
            _Context = context;
        }

        public virtual async Task Add(T obj)
        {
            await _Context.Set<T>().AddAsync(obj);
        }

        public virtual Task<IQueryable<T>> GetAll()
        {
            return Task.FromResult(_Context.Set<T>().AsQueryable());
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }

        public virtual Task Remove(T obj)
        {
            ClearChangeTrack<T>();

            return Task.FromResult(_Context.Set<T>().Remove(obj));
        }

        public virtual Task Update(T obj)
        {
            ClearChangeTrack<T>();

            _Context.Set<T>().Update(obj);

            return Task.CompletedTask;
        }

        public async void Dispose()
        {
            await _Context.DisposeAsync();
        }

        protected void ClearChangeTrack<E>() where E : class
        {
            var entry = _Context.ChangeTracker.Entries<E>().FirstOrDefault();
            if (!entry.IsNull()) entry.State = EntityState.Detached;
        }

    }
}