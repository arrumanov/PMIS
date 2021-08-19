using AutoMapper;
using Command = Dogovor.Infrastructure.Database.Command.Model;
using Query = Dogovor.Infrastructure.Database.Query.Model;
using Dogovor.Domain.Model;

namespace Dogovor.Domain.Service.Mappings
{
    public class ContragentProfile : Profile
    {
        public ContragentProfile()
        {
            CreateMap<Contragent, Command.Contragent>();

            CreateMap<Command.Contragent, Contragent>();

            CreateMap<Contragent, Query.Contragent>();
            CreateMap<Command.Contragent, Query.Contragent>();
        }
    }
}