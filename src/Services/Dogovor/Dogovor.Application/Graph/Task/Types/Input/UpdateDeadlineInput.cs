using Dogovor.Application.Commands.Task;
using GraphQL.Types;

namespace Dogovor.Application.Graph.Task.Types.Input
{
    public class UpdateDeadlineInput : InputObjectGraphType<UpdateDeadlineCommand>
    {
        public UpdateDeadlineInput()
        {
            Field<StringGraphType>().Name("id");
            Field(i => i.Deadline);
        }
    }
}
