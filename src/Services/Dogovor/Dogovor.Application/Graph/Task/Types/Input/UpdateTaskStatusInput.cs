using Dogovor.Application.Commands.Task;
using Dogovor.Application.Graph.Common;
using GraphQL.Types;

namespace Dogovor.Application.Graph.Task.Types.Input
{
    public class UpdateTaskStatusInput : InputObjectGraphType<UpdateTaskStatusCommand>
    {
        public UpdateTaskStatusInput()
        {
            Field<StringGraphType>().Name("id");
            Field<TaskStatusEnumType>().Name("status");
        }
    }
}
