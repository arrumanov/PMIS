using AutoMapper;
using Command = Dogovor.Infrastructure.Database.Command.Model;
using Query = Dogovor.Infrastructure.Database.Query.Model.User;
using System.Linq;
using Dogovor.Domain.Model;

namespace Dogovor.Domain.Service.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, Command.User>();

            CreateMap<Command.User, User>()
                .ForMember(i => i.Projects, j => j.MapFrom(m => m.UserProjects.Select(s => s.Project)));

            CreateMap<Command.User, Query.User>()
                .ForMember(i => i.Projects, j => j.MapFrom(m => m.UserProjects.Select(s => s.Project)));

            CreateMap<User, Query.User>()
                .ForMember(i => i.Projects, j => j.MapFrom(m => m.Projects));

            CreateMap<Project, Query.UserProject>();
        }
    }
}