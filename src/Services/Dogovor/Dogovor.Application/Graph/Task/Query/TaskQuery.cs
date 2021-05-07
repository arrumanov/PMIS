using System;
using System.Collections.Generic;
using Dogovor.Application.Graph.Common;
using Dogovor.CrossCutting.Extensions.GraphQL;
using Dogovor.Infrastructure.Database.Query.Manager;
using Dogovor.Application.Graph.Task.Types.Input;
using Dogovor.Application.Graph.Task.Types.Query;
using GraphQL.Types;
using Model = Dogovor.Infrastructure.Database.Query.Model.Task;

namespace Dogovor.Application.Graph.Task.Query
{
    public class TaskQuery : ObjectGraphType
    {
        public TaskQuery(IEntityManager<Model.Task> query)
        {
            Func<ResolveFieldContext, string, object> taskById = (context, id) => query.GetById(Guid.Parse(id), context.SubFields.ParseSubFields());

            Func<ResolveFieldContext, object> tasks = context => query.Get
                                                                       (
                                                                           context.SubFields.ParseSubFields(),
                                                                           context.GetArgument<IDictionary<string, object>>("filter").ParseArgumentFilter(),
                                                                           context.GetArgument<string>("order"),
                                                                           context.GetArgument<Pagination>("pagination").Skip,
                                                                           context.GetArgument<Pagination>("pagination").Take
                                                                       )
                                                                       .Result;

            FieldDelegate<TaskType>(
                "task",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the task" }
                ),
                resolve: taskById
            );

            FieldDelegate<ListGraphType<TaskType>>(
                "tasks",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PaginationType>> { Name = "pagination" },
                    new QueryArgument<StringGraphType> { Name = "order" },
                    new QueryArgument<TaskFilterType> { Name = "filter" }
                ),
                resolve: tasks
            );
        }
    }
}
