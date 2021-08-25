using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Dogovor.Application.Query.Contragent;
using Dogovor.CrossCutting.Extensions.GraphQL;
using HotChocolate.Types;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Dogovor.Application.Graph.Contragent.Query
{
    [ExtendObjectType(Name = "Query")]
    public class ContragentQuery
    {
        private readonly ILogger<ContragentQuery> _logger;

        public ContragentQuery(ILogger<ContragentQuery> logger)
        {
            _logger = logger;
        }

        [UseFiltering()]
        public async Task<IEnumerable<Infrastructure.Database.Query.Model.Contragent>> GetContragents(
            Dictionary<string, GraphFilter> filters,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var getContragentCommand = new GetContragentCommand
            {
                GraphFilters = filters
            };
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                return (await mediator.Send(getContragentCommand, cancellationToken)).ToList();
            }
        }

        public async Task<Infrastructure.Database.Query.Model.Contragent> GetContragentById(
            [GraphQLType(typeof(IdType))] Guid id,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var getContragentByIdQuery = new GetContragentByIdQuery
            {
                Id = id
            };
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                return await mediator.Send(getContragentByIdQuery, cancellationToken);
            }
        }

        public async Task<IEnumerable<Infrastructure.Database.Query.Model.Contragent>> GetContragentsOfProject(
            string ids,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var guids = JsonConvert.DeserializeObject<List<Guid>>(ids);
            var getContragentsByIdsQuery = new GetContragentsByIdsQuery
            {
                Ids = guids
            };
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                return await mediator.Send(getContragentsByIdsQuery, cancellationToken);
            }
        }
    }
}