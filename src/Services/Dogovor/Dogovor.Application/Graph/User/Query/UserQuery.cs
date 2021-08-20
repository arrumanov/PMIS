using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Dogovor.Application.Query.User;
using Dogovor.CrossCutting.Extensions.GraphQL;
using HotChocolate.Types;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Dogovor.Application.Graph.User.Query
{
    [ExtendObjectType(Name = "Query")]
    public class UserQuery
    {
        private readonly ILogger<UserQuery> _logger;

        public UserQuery(ILogger<UserQuery> logger)
        {
            _logger = logger;
        }

        [UseFiltering()]
        public async Task<IEnumerable<Infrastructure.Database.Query.Model.User>> GetUsers(
            Dictionary<string, GraphFilter> filters,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var getUserCommand = new GetUserCommand
            {
                GraphFilters = filters
            };
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                return (await mediator.Send(getUserCommand, cancellationToken)).ToList();
            }
        }
    }
}