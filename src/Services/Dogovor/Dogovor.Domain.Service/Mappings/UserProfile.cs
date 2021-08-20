using AutoMapper;
using Command = Dogovor.Infrastructure.Database.Command.Model;
using Query = Dogovor.Infrastructure.Database.Query.Model;
using Dogovor.Domain.Model;

namespace Dogovor.Domain.Service.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, Command.User>()
                .ForMember(dest => dest.SmallName, opt => opt.MapFrom(c => c.Name));

            CreateMap<Command.User, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(c => c.SmallName));

            CreateMap<User, Query.User>()
                .ForMember(dest => dest.SmallName, opt => opt.MapFrom(c => c.Name));

            CreateMap<Command.User, Query.User>();
        }
    }
}