using HotChocolate.Data.Filters;

namespace Dogovor.Application.Graph.User.Types
{
    public class UserFilterInputType : FilterInputType<Infrastructure.Database.Query.Model.User>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Infrastructure.Database.Query.Model.User> descriptor)
        {
            descriptor.Ignore(t => t.Id);
        }
    }
}