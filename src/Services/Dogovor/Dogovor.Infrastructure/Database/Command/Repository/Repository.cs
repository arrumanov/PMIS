using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
using Dogovor.CrossCutting.Extensions;
using Dogovor.CrossCutting.Extensions.GraphQL;
using Dogovor.Infrastructure.Database.Command.Interfaces;
using Dogovor.Infrastructure.Database.Command.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json;

namespace Dogovor.Infrastructure.Database.Command.Repository
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

        public virtual async Task<IQueryable<T>> Get(IDictionary<string, GraphFilter> filters)
        {
            var func = GetFilter(filters);
            return await Task.FromResult(_Context.Set<T>().Where(func).AsQueryable());
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

        private Func<T, bool> GetFilter(IDictionary<string, GraphFilter> filters)
        {
            Func<T, bool> func = new Func<T, bool>(item => true);
            if (filters == null || filters.Count == 0)
                return func;

            var strExpression = "item => true";
            var globals = new GraphFilters { Filters= filters };
            var options = ScriptOptions.Default
                .AddImports("Newtonsoft.Json", "System.Collections.Generic", "System.Linq")
                .AddReferences(typeof(Contract).Assembly, typeof(JsonConvert).Assembly, typeof(List<T>).Assembly);

            filters.ForAll(item => strExpression += $" && {GetFilterType(item.Key, filters)}");
            func = CSharpScript.EvaluateAsync<Func<T, bool>>(strExpression, options, globals).GetAwaiter().GetResult();

            return func;
        }

        private string GetFilterType(string field, IDictionary<string, GraphFilter> filters)
        {
            switch (filters[field].Operation)
            {
                case "e":
                    return $"item.{field}.ToString() == \"{filters[field].StringValue}\"";
                case "in":
                    return $"Filters[\"{field}\"].StringValues.Any(v => v == item.{field}.ToString())";
                case "с":
                    return $"item.{field}.Contains(\"{filters[field].StringValue}\")";
                //case "g":
                //    return builder.Gt(field, filter.Value);
                //case "ge":
                //    return builder.Gte(field, filter.Value);
                //case "l":
                //    return builder.Lt(field, filter.Value);
                //case "le":
                //    return builder.Lte(field, filter.Value);
                case "ne":
                    return $"item.{field}.ToString() != \"{filters[field].StringValue}\"";
                default:
                    return string.Empty;
            }
        }
    }
}