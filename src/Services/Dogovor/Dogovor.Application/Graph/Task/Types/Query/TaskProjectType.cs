using GraphQL.Types;
using Model = Dogovor.Infrastructure.Database.Query.Model.Task;

namespace Dogovor.Application.Graph.Task.Types.Query
{
    public class TaskProjectType : ObjectGraphType<Model.TaskProject>
    {
        public TaskProjectType()
        {
            Field(i => i.Id);
            Field(i => i.Description);
        }
    }
}
