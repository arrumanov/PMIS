using Dogovor.Application.Graph.Common;
using GraphQL.Types;

namespace Dogovor.Application.Graph.User.Types.Input
{
    public class UserFilterType : InputObjectGraphType
    {
        public UserFilterType()
        {
            Field<FilterType>("name");
            Field<FilterType>("email");
        }
    }
}
