using HotChocolate.Data.Filters;

namespace Dogovor.Application.Graph.Product.Types
{
    public class ProductFilterInputType : FilterInputType<Infrastructure.Database.Query.Model.Product>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Infrastructure.Database.Query.Model.Product> descriptor)
        {
            descriptor.Ignore(t => t.Id);
        }
    }
}