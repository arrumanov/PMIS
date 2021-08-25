using System;
using AutoMapper;
using Command = ProjectPortfolio.Infrastructure.Database.Command.Model;
using Query = ProjectPortfolio.Infrastructure.Database.Query.Model.Project;
using System.Linq;
using ProjectPortfolio.Domain.Model;

namespace ProjectPortfolio.Domain.Service.Mappings
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, Command.Project>()
                .AfterMap((domain, entity) =>
                {
                    foreach (var user in domain.Users)
                    {
                        var userProject = new Command.UserProject() { ProjectId = domain.Id, UserId = user.Id };
                        entity.UserProjects.Add(userProject);
                    }
                });

            CreateMap<Project, Command.UserProject>()
                .ForMember(i => i.ProjectId, j => j.MapFrom(m => m.Id))
                .ForMember(i => i.UserId,
                                j => j.MapFrom(m =>
                                                m.Users.FirstOrDefault(u => u.State == DomainState.REMOVE_RELATION
                                                                            || u.State == DomainState.ADD_RELATION
                                                                      ).Id
                                              )
                          );

            CreateMap<Command.Project, Project>()
                .ForMember(i => i.Tasks, j => j.MapFrom(m => m.Tasks))
                .ForMember(i => i.Users, j => j.MapFrom(m => m.UserProjects.Select(s => s.User)))
                .ForMember(i => i.State, j => j.MapFrom(m => DomainState.FROM_DB));

            CreateMap<Project, Query.Project>()
                .ForMember(i => i.ProjectContragents, j => j.MapFrom(m => m.ContragentIds.Select(c => new Query.ProjectContragent{ContragentId = c.ToString().ToLower()})))
                .ForMember(i => i.Tasks, j => j.MapFrom(m => m.Tasks))
                .ForMember(i => i.Participants, j => j.MapFrom(m => m.Users.Where(u => u.State != DomainState.REMOVE_RELATION)))
                .ForMember(i => i.FinishedCount, j => j.MapFrom(m => m.Tasks.Where(i => i.Status == CrossCutting.TaskStatusEnum.DONE).Count()))
                .ForMember(i => i.UnfinishedCount, j => j.MapFrom(m => m.Tasks.Where(i => i.Status != CrossCutting.TaskStatusEnum.DONE).Count()));

            CreateMap<Command.Project, Query.Project>()
                .ForMember(i => i.ProjectContragents, j => j.MapFrom(m => m.ContragentIds.Select(c => new Query.ProjectContragent { ContragentId = c.ToString().ToLower() })))
                .ForMember(i => i.Tasks, j => j.MapFrom(m => m.Tasks))
                .ForMember(i => i.Participants, j => j.MapFrom(m => m.UserProjects.Select(s => s.User)));

            CreateMap<Task, Query.ProjectTask>()
                .ForMember(i => i.Responsible, j => j.MapFrom(m => m.Assignee.Name));

            CreateMap<User, Query.ProjectUser>();

        }
    }
}
