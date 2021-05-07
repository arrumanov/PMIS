using Dogovor.Application.Commands.User;
using GraphQL.Types;

namespace Dogovor.Application.Graph.User.Types.Input
{
    public class AddUserInput : InputObjectGraphType<AddUserCommand>
    {
        public AddUserInput()
        {
            Name = "UserInput";
            Field(f => f.Name);
            Field(f => f.Email);
        }
    }
}
