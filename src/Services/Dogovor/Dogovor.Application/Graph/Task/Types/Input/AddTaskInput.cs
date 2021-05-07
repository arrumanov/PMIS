using Dogovor.Application.Commands.Task;
using GraphQL.Types;

namespace Dogovor.Application.Graph.Task.Types.Input
{
    public class AddTaskInput : InputObjectGraphType<AddTaskCommand>
    {
        public AddTaskInput()
        {
            Field(i => i.Description);
            Field(i => i.LongDescription);
            Field(i => i.DeadLine);
            Field<StringGraphType>("projectId");
            Field<StringGraphType>("reporterId");
            Field<StringGraphType>("assigneeId");

        }
    }
}
