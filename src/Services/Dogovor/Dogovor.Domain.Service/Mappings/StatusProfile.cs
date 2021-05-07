using AutoMapper;
using Dogovor.CrossCutting;
using Dogovor.Infrastructure.Database.Command.Model;

namespace Dogovor.Domain.Service.Mappings
{
    public class StatusProfile : Profile
    {
        public StatusProfile()
        {
            CreateMap<Status, TaskStatusEnum>()
                .AfterMap((model, status) =>
                {
                    status = (TaskStatusEnum)model.Id;
                });
        }
    }
}
