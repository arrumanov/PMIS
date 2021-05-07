using AutoMapper;
using Command = Dogovor.Infrastructure.Database.Command.Model;
using Query = Dogovor.Infrastructure.Database.Query.Model.Task;
using Dogovor.CrossCutting.Extensions;
using Dogovor.Domain.Model;
using Dogovor.CrossCutting;

namespace Dogovor.Domain.Service.Mappings
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, Command.Task>()
                .ForMember(i => i.ProjectId, j => j.MapFrom(m => m.Project.Id))
                .ForMember(i => i.ReporterId, j => j.MapFrom(m => m.Reporter.Id))
                .ForMember(i => i.AssigneeId, j => j.MapFrom(m => m.Assignee.Id))
                .ForMember(i => i.StatusId, j => j.MapFrom(m => (int)m.Status))
                .ForMember(i => i.Project, j => j.Ignore())
                .ForMember(i => i.Reporter, j => j.Ignore())
                .ForMember(i => i.Status, j => j.Ignore())
                .ForMember(i => i.Assignee, j => j.Ignore());

            CreateMap<Command.Task, Task>()
                .ForCtorParam("status", j => j.MapFrom(m => (TaskStatusEnum)m.StatusId))
                .ForMember(i => i.State, j => j.MapFrom(m => DomainState.FROM_DB))
                .ForMember(i => i.Project, j => j.MapFrom(m => m.Project))
                .ForMember(i => i.Assignee, j => j.MapFrom(m => m.Assignee))
                .ForMember(i => i.Status, j => j.Ignore())
                .ForMember(i => i.Reporter, j => j.MapFrom(m => m.Reporter));

            CreateMap<Task, Query.Task>()
                .ForMember(i => i.DeadLine, j => j.MapFrom(m => m.DeadLine.ToUnixTime()))
                .ForMember(i => i.CreatedDate, j => j.MapFrom(m => m.CreatedDate.ToUnixTime()))
                .ForMember(i => i.Status, j => j.MapFrom(m => m.Status))
                .ForMember(i => i.Assignee, j => j.MapFrom(m => m.Assignee))
                .ForMember(i => i.Reporter, j => j.MapFrom(m => m.Reporter))
                .ForMember(i => i.Project, j => j.MapFrom(m => m.Project));

            CreateMap<User, Query.TaskUser>()
                .ForMember(i => i.Id, j => j.MapFrom(m => m.Id))
                .ForMember(i => i.Name, j => j.MapFrom(m => m.Name));

            CreateMap<Project, Query.TaskProject>()
                .ForMember(i => i.Id, j => j.MapFrom(m => m.Id))
                .ForMember(i => i.Description, j => j.MapFrom(m => m.Description));

        }
    }
}
