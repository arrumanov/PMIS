using Dogovor.Infrastructure.Database.Command.Interfaces;
using Dogovor.Infrastructure.Database.Command.Model;

namespace Dogovor.Infrastructure.Database.Command.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(GraphContext context) : base(context)
        {
        }
    }
}