using HotChocolate.Data.Filters;

namespace Workflow.Api.Graph.Project.Types
{
    public class ProjectFilterInputType : FilterInputType<Domain.ProjectWf>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Domain.ProjectWf> descriptor)
        {
            descriptor.Ignore(t => t.Id);
        }
    }
}