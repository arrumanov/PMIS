using GraphQL.Types;

namespace Dogovor.Application.Graph.Common
{
    public class MutationResultType : ObjectGraphType
    {
        public MutationResultType()
        {
            Field<bool>("result", (a) => true);
        }
    }
}