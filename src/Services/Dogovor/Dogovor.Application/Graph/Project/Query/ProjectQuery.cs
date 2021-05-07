using System;
using System.Collections.Generic;
using Dogovor.Application.Graph.Common;
using Dogovor.Application.Graph.Project.Types.Input;
using Dogovor.Application.Graph.Project.Types.Query;
using Dogovor.CrossCutting.Extensions.GraphQL;
using Dogovor.Infrastructure.Database.Query.Manager;
using GraphQL.Types;
using Model = Dogovor.Infrastructure.Database.Query.Model.Project;

namespace Dogovor.Application.Graph.Project.Query
{
    public class ProjectQuery : ObjectGraphType
    {
        public ProjectQuery(IEntityManager<Model.Project> query)
        {
            Func<ResolveFieldContext, string, object> projectById = (context, id) => query.GetById(Guid.Parse(id), context.SubFields.ParseSubFields());

            Func<ResolveFieldContext, object> projects = context => query.Get
                                                                       (
                                                                           context.SubFields.ParseSubFields(),
                                                                           context.GetArgument<IDictionary<string, object>>("filter").ParseArgumentFilter(),
                                                                           context.GetArgument<string>("order"),
                                                                           context.GetArgument<Pagination>("pagination").Skip,
                                                                           context.GetArgument<Pagination>("pagination").Take
                                                                       )
                                                                       .Result;

            FieldDelegate<ProjectType>(
                "project",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the project" }
                ),
                resolve: projectById
            );

            FieldDelegate<ListGraphType<ProjectType>>(
                "projects",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PaginationType>> { Name = "pagination" },
                    new QueryArgument<StringGraphType> { Name = "order" },
                    new QueryArgument<ProjectFilterType> { Name = "filter" }
                ),
                resolve: projects
            );
        }
    }
}