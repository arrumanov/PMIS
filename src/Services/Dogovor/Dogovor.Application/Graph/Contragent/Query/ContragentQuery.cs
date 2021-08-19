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
    }
}