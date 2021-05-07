using GraphQL.Types;
using Model = Dogovor.Infrastructure.Database.Query.Model.Task;

namespace Dogovor.Application.Graph.Task.Types.Query
{
    public class TaskUserType : ObjectGraphType<Model.TaskUser>
    {
        public TaskUserType()
        {
            Field(i => i.Id);
            Field(i => i.Name);
        }
    }
}
