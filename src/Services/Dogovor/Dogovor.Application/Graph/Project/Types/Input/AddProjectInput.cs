using Dogovor.Application.Commands.Project;
using GraphQL.Types;

namespace Dogovor.Application.Graph.Project.Types.Input
{
    public class AddProjectInput : InputObjectGraphType<AddProjectCommand>
    {
        public AddProjectInput()
        {
            Field<NonNullGraphType<StringGraphType>>().Name("description");
            Field<StringGraphType>().Name("longDescription");
        }
    }
}