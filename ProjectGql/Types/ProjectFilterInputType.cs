using HotChocolate.Data.Filters;
using PMIS.ProjectGql.Data;

namespace PMIS.ProjectGql.Types
{
    public class ProjectFilterInputType : FilterInputType<Project>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Project> descriptor)
        {
            descriptor.Ignore(t => t.Id);
        }
    }
}