using HotChocolate.Data.Filters;

namespace ProjectPortfolio.Application.Graph.Project.Types
{
    public class ProjectFilterInputType : FilterInputType<Infrastructure.Database.Query.Model.Project.Project>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Infrastructure.Database.Query.Model.Project.Project> descriptor)
        {
            descriptor.Ignore(t => t.Id);
        }
    }
}