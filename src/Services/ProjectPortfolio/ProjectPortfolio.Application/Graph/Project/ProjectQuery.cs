using System;
using System.Threading.Tasks;
using ProjectPortfolio.Infrastructure.Database.Query.Manager;
using HotChocolate;
using HotChocolate.Data;
using System.Linq;

namespace ProjectPortfolio.Application.Graph.Project
{
    public class ProjectQuery
    {
        [UseFiltering()]
        public Task<IQueryable<Infrastructure.Database.Query.Model.Project.Project>> GetProjects(
            [Service] IEntityManager<Infrastructure.Database.Query.Model.Project.Project> query)
        {
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