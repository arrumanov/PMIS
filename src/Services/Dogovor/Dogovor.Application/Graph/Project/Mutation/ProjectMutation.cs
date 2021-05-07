using System;
using Dogovor.Application.Commands.Project;
using Dogovor.Application.Graph.Common;
using Dogovor.Application.Graph.Project.Types.Input;
using GraphQL.Types;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Dogovor.Application.Graph.Project.Mutation
{
    public class ProjectMutation : ObjectGraphType
    {
        public ProjectMutation(IServiceProvider serviceProvider)
        {
            Name = "projectMutation";
            Field<MutationResultType>(
                "addProject",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<AddProjectInput>> { Name = "data" }
                ),
                resolve: context =>
                {
                    var command = context.GetArgument<AddProjectCommand>("data");

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        return mediator.Send(command).Result;
                    }
                });

            Field<MutationResultType>(
                "addUserProject",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserProjectInput>> { Name = "data" }
                ),
                resolve: context =>
                {
                    var command = context.GetArgument<AddUserProjectCommand>("data");

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        return mediator.Send(command).Result;
                    }
                });

            Field<MutationResultType>(
                "removeUserProject",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserProjectInput>> { Name = "data" }
                ),
                resolve: context =>
                {
                    var command = context.GetArgument<RemoveUserProjectCommand>("data");

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        return mediator.Send(command).Result;
                    }
                });

            Field<MutationResultType>(
                "updateProjectInfo",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UpdateDescriptionInput>> { Name = "data" }
                ),
                resolve: context =>
                {
                    var command = context.GetArgument<UpdateProjectInfoCommand>("data");

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        return mediator.Send(command).Result;
                    }
                });
        }
    }
}