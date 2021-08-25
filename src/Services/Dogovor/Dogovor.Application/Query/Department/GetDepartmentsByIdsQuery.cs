using System;
using System.Collections.Generic;

namespace Dogovor.Application.Query.Department
{
    public class GetDepartmentsByIdsQuery : QueryBase<IEnumerable<Infrastructure.Database.Query.Model.Department>>
    {
        public List<Guid> Ids { get; set; }
    }
}