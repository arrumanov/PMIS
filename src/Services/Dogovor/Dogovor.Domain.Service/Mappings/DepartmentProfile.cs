using AutoMapper;
using Command = Dogovor.Infrastructure.Database.Command.Model;
using Query = Dogovor.Infrastructure.Database.Query.Model;
using Dogovor.Domain.Model;

namespace Dogovor.Domain.Service.Mappings
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, Command.Department>();

            CreateMap<Command.Department, Department>();

            CreateMap<Department, Query.Department>();
            CreateMap<Command.Department, Query.Department>();
        }
    }
}