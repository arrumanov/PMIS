using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using PMIS.ProjectGql.Data;
using PMIS.ProjectGql.DataLoader;
using PMIS.ProjectGql.Extensions;

namespace PMIS.ProjectGql.Projects
{
    [ExtendObjectType(Name = "Query")]
    public class ProjectQueries
    {
        [UseProjectPortfolioDbContext]
        [UsePaging]
        public IQueryable<Project> GetProjects(
            [ScopedService] ProjectPortfolioDbContext context) =>
            context.Projects.OrderBy(p => p.Name);

        [UseProjectPortfolioDbContext]
        public Task<Project> GetProjectByNameAsync(
            string name,
            [ScopedService] ProjectPortfolioDbContext context,
            CancellationToken cancellationToken) =>
            context.Projects.FirstAsync(p => p.Name == name);

        [UseProjectPortfolioDbContext]
        public async Task<IEnumerable<Project>> GetProjectByNamesAsync(
            string[] names,
            [ScopedService] ProjectPortfolioDbContext context,
            CancellationToken cancellationToken) =>
            await context.Projects.Where(p => names.Contains(p.Name)).ToListAsync();

        public Task<Project> GetProjectByIdAsync(
            [ID/*(nameof(Project))*/] Guid id,
            ProjectByIdDataLoader projectById,
            CancellationToken cancellationToken) =>
            projectById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Project>> GetProjectsByIdAsync(
            [ID/*(nameof(Project))*/] Guid[] ids,
            ProjectByIdDataLoader projectById,
            CancellationToken cancellationToken) =>
            await projectById.LoadAsync(ids, cancellationToken);
    }
}