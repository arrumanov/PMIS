using System.Reflection;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using PMIS.DogovorGql.Data;

namespace PMIS.DogovorGql.Extensions
{
    public class UseDogovorDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member)
        {
            descriptor.UseDbContext<DogovorDbContext>();
        }
    }
}