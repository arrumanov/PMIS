using System.Collections.Generic;
using Dogovor.Application.Graph.Common;

namespace Dogovor.Application.Graph.Product.Types
{
    public class ProductPayloadBase : Payload
    {
        public ProductPayloadBase(Infrastructure.Database.Query.Model.Product product)
        {
            Product = product;
        }

        public ProductPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Infrastructure.Database.Query.Model.Product? Product { get; }
    }
}