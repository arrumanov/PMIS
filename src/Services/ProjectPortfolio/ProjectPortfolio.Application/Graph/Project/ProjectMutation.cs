using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectPortfolio.Application.Commands.Project;

namespace ProjectPortfolio.Application.Graph.Project
{
    //[ExtendObjectType(Name = "Mutation")]
    public class ProjectMutation
    {
        public async Task<AddProjectPayload> AddProject(
            AddProjectInput input,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var addProjectCommand = new AddProjectCommand()
            {
                Description = input.Description,
                LongDescription = input.LongDescription
            };
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                var t = await mediator.Send(addProjectCommand, cancellationToken);
                return new AddProjectPayload(new Infrastructure.Database.Query.Model.Project.Project());
            }
        }

        public async Task<AddProjectPayload> AddUserProject(
            UserProjectInput input,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var addUserProjectCommand = new AddUserProjectCommand()
            {
                ProjectId = input.ProjectId,
                UserId = input.UserId
            };
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                var t = await mediator.Send(addUserProjectCommand, cancellationToken);
                return new AddProjectPayload(new Infrastructure.Database.Query.Model.Project.Project());
            }
        }

        public async Task<AddProjectPayload> RemoveUserProject(
            UserProjectInput input,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var removeUserProjectCommand = new RemoveUserProjectCommand()
            {
                ProjectId = input.ProjectId,
                UserId = input.UserId
            };
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                var t = await mediator.Send(removeUserProjectCommand, cancellationToken);
                return new AddProjectPayload(new Infrastructure.Database.Query.Model.Project.Project());
            }
        }

        public async Task<AddProjectPayload> UpdateProjectInfo(
            UpdateProjectInfoInput input,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var updateProjectInfoCommand = new UpdateProjectInfoCommand()
            {
                Id = input.Id,
                Description = input.Description,
                LongDescription = input.LongDescription
            };
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                var t = await mediator.Send(updateProjectInfoCommand, cancellationToken);
                return new AddProjectPayload(new Infrastructure.Database.Query.Model.Project.Project());
            }
        }
    }
}