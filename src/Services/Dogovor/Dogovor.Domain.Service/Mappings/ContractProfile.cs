using AutoMapper;
using Command = Dogovor.Infrastructure.Database.Command.Model;
using Query = Dogovor.Infrastructure.Database.Query.Model.Contract;
using Dogovor.CrossCutting.Extensions;
using Dogovor.Domain.Model;
using Dogovor.CrossCutting;

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