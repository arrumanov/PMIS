using HotChocolate.Data.Filters;

namespace Workflow.Api.Graph.Project.Types
{
    public class ProjectFilterInputType : FilterInputType<Domain.Project>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Domain.Project> descriptor)
        {
            descriptor.Ignore(t => t.Id);
        }
    }
}