using HotChocolate.Data.Filters;

namespace ProjectPortfolio.Application.Graph.Project
{
    public class ProjectFilterInputType : FilterInputType<Infrastructure.Database.Query.Model.Project.Project>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Infrastructure.Database.Query.Model.Project.Project> descriptor)
        {
            descriptor.Ignore(t => t.Id);
        }
    }
}