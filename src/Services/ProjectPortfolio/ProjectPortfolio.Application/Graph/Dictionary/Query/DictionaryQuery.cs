using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using ProjectPortfolio.Infrastructure.Database.Query.Manager;

namespace ProjectPortfolio.Application.Graph.Dictionary.Query
{
    [ExtendObjectType(Name = "Query")]
    public class DictionaryQuery
    {
        //[UseFiltering()]
        public IEnumerable<Infrastructure.Database.Query.Model.Dictionary.Dictionary> GetDictionarys(
            [Service] IEntityManager<Infrastructure.Database.Query.Model.Dictionary.Dictionary> query)
        {
            return query.GetList();
        }

        //[UseFiltering()]
        public Task<Infrastructure.Database.Query.Model.Dictionary.Dictionary> GetDictionaryById(
            [Service] IEntityManager<Infrastructure.Database.Query.Model.Dictionary.Dictionary> query, [GraphQLType(typeof(IdType))] Guid id)
        {
            return query.GetById(id);
        }
    }
}