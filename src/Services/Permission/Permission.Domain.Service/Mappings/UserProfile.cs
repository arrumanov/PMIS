using AutoMapper;
using Command = Permission.Infrastructure.Database.Command.Model;
using Query = Permission.Infrastructure.Database.Query.Model;
using Permission.Domain.Model;

namespace Permission.Domain.Service.Mappings
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