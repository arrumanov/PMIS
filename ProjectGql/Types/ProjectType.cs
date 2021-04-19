using HotChocolate.Resolvers;
using HotChocolate.Types;
using PMIS.ProjectGql.Data;
using PMIS.ProjectGql.DataLoader;

namespace PMIS.ProjectGql.Types
{
    public class ProjectType : ObjectType<Project>
    {
        protected override void Configure(IObjectTypeDescriptor<Project> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) =>
                    ctx.DataLoader<ProjectByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));
        }
    }
}