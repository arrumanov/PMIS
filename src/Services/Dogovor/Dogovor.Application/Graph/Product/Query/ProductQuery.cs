using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Dogovor.Application.Query.Product;
using Dogovor.CrossCutting.Extensions.GraphQL;
using HotChocolate.Types;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Dogovor.Application.Graph.Product.Query
{
    [ExtendObjectType(Name = "Query")]
    public class ProductQuery
    {
        private readonly ILogger<ProductQuery> _logger;

        public ProductQuery(ILogger<ProductQuery> logger)
        {
            _logger = logger;
        }

        [UseFiltering()]
        public async Task<IEnumerable<Infrastructure.Database.Query.Model.Product>> GetProducts(
            Dictionary<string, GraphFilter> filters,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var getProductCommand = new GetProductCommand
            {
                GraphFilters = filters
            };
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                return (await mediator.Send(getProductCommand, cancellationToken)).ToList();
            }
        }

        public async Task<Infrastructure.Database.Query.Model.Product> GetProductById(
            string id,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var getProductByIdQuery = new GetProductByIdQuery
            {
                Id = Guid.Parse(id)
            };
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                return await mediator.Send(getProductByIdQuery, cancellationToken);
            }
        }
    }
}