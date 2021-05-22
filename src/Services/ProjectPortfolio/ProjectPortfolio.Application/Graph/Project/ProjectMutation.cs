using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.Internal;
using HotChocolate;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectPortfolio.Application.Commands.Project;
using ProjectPortfolio.Application.Graph.Common;
using ProjectPortfolio.CrossCutting.Exceptions;

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
                try
                {
                    var response = await mediator.Send(addProjectCommand, cancellationToken);
                    return new AddProjectPayload(response);
                }
                catch (ValidationException e)
                {
                    var userErrors = new List<UserError>();
                    e.Message.Split(";").ForAll(item =>
                    {
                        userErrors.Add(new UserError(item, item));
                    });
                    return new AddProjectPayload(userErrors);
                }
            }
        }

        public async Task<AddUserProjectPayload> AddUserProject(
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
                try
                {
                    var response = await mediator.Send(addUserProjectCommand, cancellationToken);
                    return new AddUserProjectPayload(response.Project, response.User);
                }
                catch (ValidationException e)
                {
                    var userErrors = new List<UserError>();
                    e.Message.Split(";").ForAll(item =>
                    {
                        userErrors.Add(new UserError(item, item));
                    });
                    return new AddUserProjectPayload(userErrors);
                }
            }
        }

        public async Task<AddUserProjectPayload> RemoveUserProject(
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
                try
                {
                    var response = await mediator.Send(removeUserProjectCommand, cancellationToken);
                    return new AddUserProjectPayload(response.Project, response.User);
                }
                catch (ValidationException e)
                {
                    var userErrors = new List<UserError>();
                    e.Message.Split(";").ForAll(item =>
                    {
                        userErrors.Add(new UserError(item, item));
                    });
                    return new AddUserProjectPayload(userErrors);
                }
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
                try
                {
                    var response = await mediator.Send(updateProjectInfoCommand, cancellationToken);
                    return new AddProjectPayload(response);
                }
                catch (ValidationException e)
                {
                    var userErrors = new List<UserError>();
                    e.Message.Split(";").ForAll(item => { userErrors.Add(new UserError(item, item)); });
                    return new AddProjectPayload(userErrors);
                }
                catch (ElementNotFoundException e)
                {
                    return new AddProjectPayload(new List<UserError>
                    {
                        new UserError("Элемент не найден", "PROJ-not_found")
                    });
                }
                catch (QueryArgumentException e)
                {
                    return new AddProjectPayload(new List<UserError>
                    {
                        new UserError(e.Message, e.Message)
                    });
                }
            }
        }
    }
}