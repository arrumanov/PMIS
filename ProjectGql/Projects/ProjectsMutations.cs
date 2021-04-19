using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using PMIS.ProjectGql.Data;
using PMIS.ProjectGql.Extensions;

namespace PMIS.ProjectGql.Projects
{
    [ExtendObjectType(Name = "Mutation")]
    public class ProjectsMutations
    {
        [UseProjectPortfolioDbContext]
        public async Task<AddProjectPayload> AddProjectAsync(
            AddProjectInput input,
            [ScopedService] ProjectPortfolioDbContext context,
            CancellationToken cancellationToken)
        {
            var project = new Project
            {
                Name = input.Name,
                CreatedDate = DateTime.Today
        };
            context.Projects.Add(project);

            await context.SaveChangesAsync(cancellationToken);

            return new AddProjectPayload(project);
        }

        [UseProjectPortfolioDbContext]
        public async Task<RenameProjectPayload> RenameProjectAsync(
            RenameProjectInput input,
            [ScopedService] ProjectPortfolioDbContext context,
            CancellationToken cancellationToken)
        {
            Project project = await context.Projects.FindAsync(input.Id);
            project.Name = input.Name;

            await context.SaveChangesAsync(cancellationToken);

            return new RenameProjectPayload(project);
        }
    }
}