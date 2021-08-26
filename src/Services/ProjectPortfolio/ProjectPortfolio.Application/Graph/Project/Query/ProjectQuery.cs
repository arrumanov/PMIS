using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectPortfolio.Infrastructure.Database.Query.Manager;
using HotChocolate;
using HotChocolate.Data;
using System.Linq;
using HotChocolate.Types;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ProjectPortfolio.Application.Graph.Project.Query
{
    [ExtendObjectType(Name = "Query")]
    public class ProjectQuery
    {
        private readonly ILogger<ProjectQuery> _logger;

        public ProjectQuery(ILogger<ProjectQuery> logger)
        {
            _logger = logger;
        }

        [UseFiltering()]
        public IEnumerable<Infrastructure.Database.Query.Model.Project.Project> GetProjects(
            [Service] IEntityManager<Infrastructure.Database.Query.Model.Project.Project> query)
        {
            _logger.LogInformation("GetProjects");
            return query.GetList();
        }

        [UseFiltering()]
        public Task<Infrastructure.Database.Query.Model.Project.Project> GetProjectById(
            [Service] IEntityManager<Infrastructure.Database.Query.Model.Project.Project> query, [GraphQLType(typeof(IdType))] Guid id)
        {
            return query.GetById(id);
        }

    }
}