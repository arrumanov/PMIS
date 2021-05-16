using AutoMapper;
using ProjectPortfolio.CrossCutting;
using ProjectPortfolio.Infrastructure.Database.Command.Model;

namespace ProjectPortfolio.Domain.Service.Mappings
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
