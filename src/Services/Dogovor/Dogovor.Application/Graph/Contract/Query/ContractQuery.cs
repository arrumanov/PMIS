using System;
using System.Linq;
using Dogovor.Infrastructure.Database.Query.Manager;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using HotChocolate.Types;

namespace Dogovor.Application.Graph.Contract.Query
{
    //[ExtendObjectType(Name = "Query")]
    public class ContractQuery
    {
        private readonly ILogger<ContractQuery> _logger;

        public ContractQuery(ILogger<ContractQuery> logger)
        {
            _logger = logger;
        }

        [UseFiltering()]
        public Task<IQueryable<Infrastructure.Database.Query.Model.Contract.Contract>> GetContracts(
            [Service] IEntityManager<Infrastructure.Database.Query.Model.Contract.Contract> query)
        {
            _logger.LogInformation("GetContracts");
            return query.Get();
        }

        [UseFiltering()]
        public Task<Infrastructure.Database.Query.Model.Contract.Contract> GetContractById(
            [Service] IEntityManager<Infrastructure.Database.Query.Model.Contract.Contract> query, Guid id)
        {
            return query.GetById(id);
        }
    }
}