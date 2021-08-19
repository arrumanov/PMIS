using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Dogovor.Application.Query.Contract;
using Dogovor.CrossCutting.Extensions.GraphQL;
using HotChocolate.Types;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Dogovor.Application.Graph.Contract.Query
{
    [ExtendObjectType(Name = "Query")]
    public class ContractQuery
    {
        private readonly ILogger<ContractQuery> _logger;

        public ContractQuery(ILogger<ContractQuery> logger)
        {
            _logger = logger;
        }

        //[UseFiltering()]
        //public Task<IQueryable<Infrastructure.Database.Query.Model.Contract.Contract>> GetContracts(
        //    [Service] IEntityManager<Infrastructure.Database.Query.Model.Contract.Contract> query)
        //{
        //    _logger.LogInformation("GetContracts");
        //    return query.Get();
        //}

        //[UseFiltering()]
        //public Task<Infrastructure.Database.Query.Model.Contract.Contract> GetContractById(
        //    [Service] IEntityManager<Infrastructure.Database.Query.Model.Contract.Contract> query, Guid id)
        //{
        //    return query.GetById(id);
        //}

        //[UseFiltering()]
        //public async Task<IQueryable<Infrastructure.Database.Query.Model.Contract.Contract>> GetContracts(
        //    [Service] IServiceProvider serviceProvider,
        //    CancellationToken cancellationToken)
        //{
        //    var getContractCommand = new GetContractCommand();
        //    using (var scope = serviceProvider.CreateScope())
        //    {
        //        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        //        return await mediator.Send(getContractCommand, cancellationToken);
        //        //try
        //        //{
        //        //    return await mediator.Send(getContractCommand, cancellationToken);
        //        //}
        //        //catch (ValidationException e)
        //        //{
        //        //    var userErrors = new List<UserError>();
        //        //    e.Message.Split(";").ForAll(item =>
        //        //    {
        //        //        userErrors.Add(new UserError(item, item));
        //        //    });
        //        //    return new AddContractPayload(userErrors);
        //        //}
        //    }
        //}

        [UseFiltering()]
        public async Task<IEnumerable<Infrastructure.Database.Query.Model.Contract.Contract>> GetContracts(
            Dictionary<string, GraphFilter> filters,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var getContractCommand = new GetContractCommand
            {
                GraphFilters = filters
            };
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                return (await mediator.Send(getContractCommand, cancellationToken)).ToList();
            }
        }
    }
}