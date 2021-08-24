using System;

namespace Dogovor.Application.Query.Product
{
    public class GetProductByIdQuery : QueryBase<Infrastructure.Database.Query.Model.Product>
    {
        public Guid Id { get; set; }
    }
}