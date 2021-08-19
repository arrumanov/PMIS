using System;
using System.Collections.Generic;
using System.Linq;
using Dogovor.CrossCutting.Extensions.GraphQL;

namespace Dogovor.Application.Query.Contract
{
    public class GetContractCommand : QueryBase<IQueryable<Infrastructure.Database.Query.Model.Contract.Contract>>
    {
        public List<Guid> Id { get; set; }
        public Dictionary<string, GraphFilter> GraphFilters { get; set; }
    }
}