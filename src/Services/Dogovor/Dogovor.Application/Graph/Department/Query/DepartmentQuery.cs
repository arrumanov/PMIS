using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Dogovor.Application.Query.Department;
using Dogovor.CrossCutting.Extensions.GraphQL;
using HotChocolate.Types;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Dogovor.Application.Graph.Department.Query
{
    [ExtendObjectType(Name = "Query")]
    public class DepartmentQuery
    {
        private readonly ILogger<DepartmentQuery> _logger;

        public DepartmentQuery(ILogger<DepartmentQuery> logger)
        {
            _logger = logger;
        }

        [UseFiltering()]
        public async Task<IEnumerable<Infrastructure.Database.Query.Model.Department>> GetDepartments(
            Dictionary<string, GraphFilter> filters,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var getDepartmentCommand = new GetDepartmentCommand
            {
                GraphFilters = filters
            };
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                return (await mediator.Send(getDepartmentCommand, cancellationToken)).ToList();
            }
        }

        public async Task<Infrastructure.Database.Query.Model.Department> GetDepartmentById(
            [GraphQLType(typeof(IdType))] Guid id,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var getDepartmentByIdQuery = new GetDepartmentByIdQuery
            {
                Id = id
            };
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                return await mediator.Send(getDepartmentByIdQuery, cancellationToken);
            }
        }

        public async Task<IEnumerable<Infrastructure.Database.Query.Model.Department>> GetDepartmentsOfProject(
            string ids,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var guids = JsonConvert.DeserializeObject<List<Guid>>(ids);
            var getDepartmentByIdQuery = new GetDepartmentsByIdsQuery
            {
                Ids = guids //ids.Select(Guid.Parse).ToList()
            };
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                return await mediator.Send(getDepartmentByIdQuery, cancellationToken);
            }
        }
    }
}