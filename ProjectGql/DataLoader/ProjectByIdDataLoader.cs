using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using HotChocolate.DataLoader;
using Microsoft.EntityFrameworkCore;
using PMIS.ProjectGql.Data;

namespace PMIS.ProjectGql.DataLoader
{
    public class ProjectByIdDataLoader : BatchDataLoader<Guid, Project>
    {
        private readonly IDbContextFactory<ProjectPortfolioDbContext> _dbContextFactory;

        public ProjectByIdDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<ProjectPortfolioDbContext> dbContextFactory)
            : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory ??
                                throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<Guid, Project>> LoadBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            await using ProjectPortfolioDbContext dbContext =
                _dbContextFactory.CreateDbContext();

            return await dbContext.Projects
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(p => p.Id, cancellationToken);
        }
    }
}