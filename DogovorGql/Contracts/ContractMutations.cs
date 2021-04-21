using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using PMIS.DogovorGql.Data;
using PMIS.DogovorGql.Extensions;

namespace PMIS.DogovorGql.Contracts
{
    [ExtendObjectType(Name = "Mutation")]
    public class ContractMutations
    {
        [UseDogovorDbContext]
        public async Task<AddContractPayload> AddContractAsync(
            AddContractInput input,
            [ScopedService] DogovorDbContext context,
            CancellationToken cancellationToken)
        {
            var contract = new Contract
            {
                Name = input.Name,
                CreatedDate = DateTime.Today
            };
            context.Contracts.Add(contract);

            await context.SaveChangesAsync(cancellationToken);

            return new AddContractPayload(contract);
        }

        [UseDogovorDbContext]
        public async Task<RenameContractPayload> RenameContractAsync(
            RenameContractInput input,
            [ScopedService] DogovorDbContext context,
            CancellationToken cancellationToken)
        {
            Contract contract = await context.Contracts.FindAsync(input.Id);
            contract.Name = input.Name;

            await context.SaveChangesAsync(cancellationToken);

            return new RenameContractPayload(contract);
        }
    }
}