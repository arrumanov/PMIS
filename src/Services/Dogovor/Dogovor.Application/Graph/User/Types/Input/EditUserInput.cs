using Dogovor.Application.Commands.User;
using GraphQL.Types;

namespace Dogovor.Application.Graph.User.Types.Input
{
    public class EditUserInput : InputObjectGraphType<UpdateUserInfoCommand>
    {
        public EditUserInput()
        {
            Name = "UserInput";
            Field<StringGraphType>().Name("Id");
            Field(f => f.Name);
            Field(f => f.Email);
        }
    }
}
