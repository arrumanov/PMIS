﻿using Dogovor.CrossCutting.Extensions;
using GraphQL.Types;
using Model = Dogovor.Infrastructure.Database.Query.Model.Task;

namespace Dogovor.Application.Graph.Task.Types.Query
{
    public class TaskType : ObjectGraphType<Model.Task>
    {
        public TaskType()
        {
            Field(i => i.Id);
            Field(i => i.Description);
            Field<StringGraphType>().Name("longDescription").Resolve(ctx => ctx.Source.LongDescription ?? string.Empty);
            Field(i => i.Status);
            Field<DateGraphType>().Name("deadline").Resolve(ctx => ctx.Source.DeadLine.UnixToDateTime());
            Field<DateGraphType>().Name("createdDate").Resolve(ctx => ctx.Source.CreatedDate.UnixToDateTime());

            Field<TaskUserType>().Name("assignee").Resolve((ctx) => ctx.Source.Assignee);
            Field<TaskUserType>().Name("reporter").Resolve((ctx) => ctx.Source.Reporter);

            Field<TaskProjectType>().Name("project").Resolve((ctx) => ctx.Source.Project);
        }
    }
}
