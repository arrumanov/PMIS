using GraphQL.Types;

namespace Dogovor.Application.Graph.Project.Types.Input
{
    public class UserProjectInput : InputObjectGraphType
    {
        public UserProjectInput()
        {
            Field<IdGraphType>().Name("projectId");
            Field<IdGraphType>().Name("userId");
        }
    }
}