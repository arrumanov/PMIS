using HotChocolate.Data.Filters;

namespace Dogovor.Application.Graph.Contract.Types
{
    public class ContragentFilterInputType : FilterInputType<Infrastructure.Database.Query.Model.Contract>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Infrastructure.Database.Query.Model.Contract> descriptor)
        {
            descriptor.Ignore(t => t.Id);
        }
    }
}