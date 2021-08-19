using System;
using System.Collections.Generic;
using System.Linq;
using Dogovor.CrossCutting.Extensions.GraphQL;

namespace Dogovor.Application.Query.Contragent
{
    public class GetContragentCommand : QueryBase<IQueryable<Infrastructure.Database.Query.Model.Contragent>>
    {
        public List<Guid> Id { get; set; }
        public Dictionary<string, GraphFilter> GraphFilters { get; set; }
    }
}