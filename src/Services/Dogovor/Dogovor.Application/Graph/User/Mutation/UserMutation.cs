using System;
using Dogovor.Application.Commands.User;
using Dogovor.Application.Graph.Common;
using Dogovor.Application.Graph.User.Types.Input;
using GraphQL.Types;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Dogovor.Application.Graph.User.Mutation
{
    public class UserMutation : ObjectGraphType
    {
        public UserMutation(IServiceProvider serviceProvider)
        {
            Name = "UserMutation";
            Field<MutationResultType>(
                "addUser",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<AddUserInput>> { Name = "data" }
                ),
                resolve: context =>
                {
                    var command = context.GetArgument<AddUserCommand>("data");
                    
                    using(var scope = serviceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        return mediator.Send(command).Result;
                    }
                });

            Field<MutationResultType>(
                "editUserInfo",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<EditUserInput>> { Name = "data" }
                ),
                resolve: context =>
                {
                    var command = context.GetArgument<UpdateUserInfoCommand>("data");

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        return mediator.Send(command).Result;
                    }
                });
        }

    }
}
