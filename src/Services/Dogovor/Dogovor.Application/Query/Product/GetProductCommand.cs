using System;
using System.Collections.Generic;
using System.Linq;
using Dogovor.CrossCutting.Extensions.GraphQL;

namespace Dogovor.Application.Query.Product
{
    public class GetProductCommand : QueryBase<IQueryable<Infrastructure.Database.Query.Model.Product>>
    {
        public List<Guid> Id { get; set; }
        public Dictionary<string, GraphFilter> GraphFilters { get; set; }
    }
}