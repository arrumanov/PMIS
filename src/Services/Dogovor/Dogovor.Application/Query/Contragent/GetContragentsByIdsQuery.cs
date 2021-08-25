using System;
using System.Collections.Generic;

namespace Dogovor.Application.Query.Contragent
{
    public class GetContragentsByIdsQuery : QueryBase<IEnumerable<Infrastructure.Database.Query.Model.Contragent>>
    {
        public List<Guid> Ids { get; set; }
    }
}