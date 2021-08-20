using System;
using System.Collections.Generic;
using System.Linq;
using Dogovor.CrossCutting.Extensions.GraphQL;

namespace Dogovor.Application.Query.Department
{
    public class GetDepartmentCommand : QueryBase<IQueryable<Infrastructure.Database.Query.Model.Department>>
    {
        public List<Guid> Id { get; set; }
        public Dictionary<string, GraphFilter> GraphFilters { get; set; }
    }
}