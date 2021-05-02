using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using PMIS.Contracts;
using PMIS.DogovorGql.Data;
using PMIS.DogovorGql.DataLoader;
using PMIS.DogovorGql.Extensions;
using PMIS.DogovorGql.IntegrationEvents;

namespace PMIS.DogovorGql.Contracts
{
    [ExtendObjectType(Name = "Query")]
    public class ContractQueries
    {
        private readonly IPublishEndpoint _endpoint;
        private readonly IServiceProvider _serviceProvider;

        public ContractQueries(IPublishEndpoint endpoint, IServiceProvider serviceProvider)
        {
            _endpoint = endpoint;
            _serviceProvider = serviceProvider;
        }

        [UseDogovorDbContext]
        [UsePaging]
        public IQueryable<Contract> GetContracts(
            [ScopedService] DogovorDbContext context) =>
            context.Contracts.OrderBy(p => p.Name);

        [UseDogovorDbContext]
        public Task<Contract> GetContractByNameAsync(
            string name,
            [ScopedService] DogovorDbContext context,
            CancellationToken cancellationToken)
        {
            var project = new ProjectDto()
            {
                Name = "Project 1",
                Price = 45,
                Quantity = 99
            };
            var projectService = (IProjectService)_serviceProvider.GetService(typeof(IProjectService));
            projectService.Put(project, cancellationToken);

            return context.Contracts.FirstAsync(p => p.Name == name);
        }

        [UseDogovorDbContext]
        public async Task<IEnumerable<Contract>> GetContractByNamesAsync(
            string[] names,
            [ScopedService] DogovorDbContext context,
            CancellationToken cancellationToken) =>
            await context.Contracts.Where(p => names.Contains(p.Name)).ToListAsync();

        public Task<Contract> GetContractByIdAsync(
            [ID/*(nameof(Contract))*/] Guid id,
            ContractByIdDataLoader contractById,
            CancellationToken cancellationToken) =>
            contractById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Contract>> GetContractsByIdAsync(
            [ID/*(nameof(Contract))*/] Guid[] ids,
            ContractByIdDataLoader contractById,
            CancellationToken cancellationToken) =>
            await contractById.LoadAsync(ids, cancellationToken);
    }
}