using Dogovor.Application.Commands.Task;
using GraphQL.Types;

namespace Dogovor.Application.Graph.Task.Types.Input
{
    public class ChangeAssigneeInput : InputObjectGraphType<ChangeAssigneeCommand>
    {
        public ChangeAssigneeInput()
        {
            Field<StringGraphType>().Name("id");
            Field<StringGraphType>().Name("newAssigneeId");
        }
    }
}
