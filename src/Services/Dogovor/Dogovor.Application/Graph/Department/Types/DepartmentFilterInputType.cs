using HotChocolate.Data.Filters;

namespace Dogovor.Application.Graph.Department.Types
{
    public class DepartmentFilterInputType : FilterInputType<Infrastructure.Database.Query.Model.Department>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Infrastructure.Database.Query.Model.Department> descriptor)
        {
            descriptor.Ignore(t => t.Id);
        }
    }
}