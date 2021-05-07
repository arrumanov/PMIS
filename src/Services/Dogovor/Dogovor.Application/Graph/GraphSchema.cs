using Dogovor.Application.Graph.Project.Mutation;
using Dogovor.Application.Graph.Project.Query;
using Dogovor.Application.Graph.Task.Mutation;
using Dogovor.Application.Graph.Task.Query;
using Dogovor.Application.Graph.User.Mutation;
using Dogovor.Application.Graph.User.Query;
using Dogovor.CrossCutting.Extensions.GraphQL;
using GraphQL;
using GraphQL.Types;

namespace Dogovor.Application.Graph
{
    public class GraphSchema : Schema
    {
        public GraphSchema(IDependencyResolver resolver) : base(resolver)
        {
            this.AddQuery<UserQuery>();
            this.AddQuery<ProjectQuery>();
            this.AddQuery<TaskQuery>();

            this.AddMutation<UserMutation>();
            this.AddMutation<ProjectMutation>();
            this.AddMutation<TaskMutation>();
        }
    }
}