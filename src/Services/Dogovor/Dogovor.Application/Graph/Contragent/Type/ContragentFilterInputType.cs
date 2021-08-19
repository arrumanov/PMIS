using HotChocolate.Data.Filters;

namespace Dogovor.Application.Graph.Contragent.Types
{
    public class ContragentFilterInputType : FilterInputType<Infrastructure.Database.Query.Model.Contragent>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Infrastructure.Database.Query.Model.Contragent> descriptor)
        {
            descriptor.Ignore(t => t.Id);
        }
    }
}