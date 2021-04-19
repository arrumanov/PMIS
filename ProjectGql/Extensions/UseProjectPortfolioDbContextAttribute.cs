using System.Reflection;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using PMIS.ProjectGql.Data;

namespace PMIS.ProjectGql.Extensions
{
    public class UseProjectPortfolioDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member)
        {
            descriptor.UseDbContext<ProjectPortfolioDbContext>();
        }
    }
}