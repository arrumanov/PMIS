using System;
using Dogovor.Application.Commands.Task;
using Dogovor.Application.Graph.Common;
using Dogovor.Application.Graph.Task.Types.Input;
using GraphQL.Types;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Dogovor.Application.Graph.Task.Mutation
{
    public class TaskMutation : ObjectGraphType
    {
        public TaskMutation(IServiceProvider serviceProvider)
        {
            Field<MutationResultType>(
                "addTask",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<AddTaskInput>> { Name = "data" }
                ),
                resolve: context =>
                {
                    var command = context.GetArgument<AddTaskCommand>("data");

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        return mediator.Send(command).Result;
                    }
                });

            Field<MutationResultType>(
                "updateTaskInfo",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UpdateDescriptionInput>> { Name = "data" }
                ),
                resolve: context =>
                {
                    var command = context.GetArgument<UpdateTaskInfoCommand>("data");

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        return mediator.Send(command).Result;
                    }
                });

            Field<MutationResultType>(
                "changeAssignee",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ChangeAssigneeInput>> { Name = "data" }
                ),
                resolve: context =>
                {
                    var command = context.GetArgument<ChangeAssigneeCommand>("data");

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        return mediator.Send(command).Result;
                    }
                });

            Field<MutationResultType>(
                "updateDeadline",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UpdateDeadlineInput>> { Name = "data" }
                ),
                resolve: context =>
                {
                    var command = context.GetArgument<UpdateDeadlineCommand>("data");

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        return mediator.Send(command).Result;
                    }
                });

            Field<MutationResultType>(
                "updateTaskStatus",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UpdateTaskStatusInput>> { Name = "data" }
                ),
                resolve: context =>
                {
                    var command = context.GetArgument<UpdateTaskStatusCommand>("data");

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        return mediator.Send(command).Result;
                    }
                });
        }
    }
}
