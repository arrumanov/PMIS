using AutoMapper;
using Command = Dogovor.Infrastructure.Database.Command.Model;
using Query = Dogovor.Infrastructure.Database.Query.Model;
using Dogovor.Domain.Model;

namespace Dogovor.Domain.Service.Mappings
{
    public class ContractProfile : Profile
    {
        public ContractProfile()
        {
            CreateMap<Contract, Command.Contract>();

            CreateMap<Command.Contract, Contract>();

            CreateMap<Contract, Query.Contract>();
            CreateMap<Command.Contract, Query.Contract>();
        }
    }
}