using System;
using System.Threading.Tasks;
using ProjectPortfolio.Infrastructure.Database.Query.Manager;
using HotChocolate;
using HotChocolate.Data;
using System.Linq;
using HotChocolate.Types;
using Microsoft.Extensions.Logging;

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
        public Task<IQueryable<Infrastructure.Database.Query.Model.Project.Project>> GetProjects(
            [Service] IEntityManager<Infrastructure.Database.Query.Model.Project.Project> query)
        {
            _logger.LogInformation("GetProjects");
            return query.Get();
        }

        [UseFiltering()]
        public Task<Infrastructure.Database.Query.Model.Project.Project> GetProjectById(
            [Service] IEntityManager<Infrastructure.Database.Query.Model.Project.Project> query, Guid id)
        {
            return query.GetById(id);
        }

    }
}