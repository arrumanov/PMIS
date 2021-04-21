using HotChocolate.Resolvers;
using HotChocolate.Types;
using PMIS.DogovorGql.Data;
using PMIS.DogovorGql.DataLoader;

namespace PMIS.DogovorGql.Types
{
    public class ContractType : ObjectType<Contract>
    {
        protected override void Configure(IObjectTypeDescriptor<Contract> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) =>
                    ctx.DataLoader<ContractByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));
        }
    }
}