using System;
using System.Collections.Generic;

namespace Dogovor.Application.Query.Product
{
    public class GetProductsByIdsQuery : QueryBase<IEnumerable<Infrastructure.Database.Query.Model.Product>>
    {
        public List<Guid> Ids { get; set; }
    }
}