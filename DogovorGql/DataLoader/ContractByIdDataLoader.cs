using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using HotChocolate.DataLoader;
using Microsoft.EntityFrameworkCore;
using PMIS.DogovorGql.Data;

namespace PMIS.DogovorGql.DataLoader
{
    public class ContractByIdDataLoader : BatchDataLoader<Guid, Contract>
    {
        private readonly IDbContextFactory<DogovorDbContext> _dbContextFactory;

        public ContractByIdDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<DogovorDbContext> dbContextFactory)
            : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory ??
                                throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<Guid, Contract>> LoadBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            await using DogovorDbContext dbContext =
                _dbContextFactory.CreateDbContext();

            return await dbContext.Contracts
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(p => p.Id, cancellationToken);
        }
    }
}