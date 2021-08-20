using AutoMapper;
using Command = Dogovor.Infrastructure.Database.Command.Model;
using Query = Dogovor.Infrastructure.Database.Query.Model;
using Dogovor.Domain.Model;

namespace Dogovor.Domain.Service.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, Command.Product>();

            CreateMap<Command.Product, Product>();

            CreateMap<Product, Query.Product>();
            CreateMap<Command.Product, Query.Product>();
        }
    }
}