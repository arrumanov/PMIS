using System;
using System.Collections.Generic;
using Dogovor.Application.Graph.Common;
using Dogovor.CrossCutting.Extensions.GraphQL;
using Dogovor.Infrastructure.Database.Query.Manager;
using Dogovor.Application.Graph.User.Types.Input;
using Dogovor.Application.Graph.User.Types.Query;
using GraphQL.Types;
using Model = Dogovor.Infrastructure.Database.Query.Model.User;

namespace Dogovor.Application.Graph.User.Query
{
    public class UserQuery : ObjectGraphType
    {
        public UserQuery(IEntityManager<Model.User> query)
        {
            Func<ResolveFieldContext, string, object> userById = (context, id) => query.GetById(Guid.Parse(id), context.SubFields.ParseSubFields());

            Func<ResolveFieldContext, object> users = context => query.Get
                                                                       (   
                                                                           context.SubFields.ParseSubFields(),
                                                                           context.GetArgument<IDictionary<string, object>>("filter").ParseArgumentFilter(),
                                                                           context.GetArgument<string>("order"),
                                                                           context.GetArgument<Pagination>("pagination").Skip,
                                                                           context.GetArgument<Pagination>("pagination").Take
                                                                       )
                                                                       .Result;

            FieldDelegate<UserType>(
                "user",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the user" }
                ),
                resolve: userById
            );

            FieldDelegate<ListGraphType<UserType>>(
                "users",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PaginationType>> { Name = "pagination" },
                    new QueryArgument<StringGraphType> { Name = "order" },
                    new QueryArgument<UserFilterType> { Name = "filter" }
                ),
                resolve: users
            );
        }
    }
}
