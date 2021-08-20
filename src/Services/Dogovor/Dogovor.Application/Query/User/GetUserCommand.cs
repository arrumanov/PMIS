using System;
using System.Collections.Generic;
using System.Linq;
using Dogovor.CrossCutting.Extensions.GraphQL;

namespace Dogovor.Application.Query.User
{
    public class GetUserCommand : QueryBase<IQueryable<Infrastructure.Database.Query.Model.User>>
    {
        public List<Guid> Id { get; set; }
        public Dictionary<string, GraphFilter> GraphFilters { get; set; }
    }
}