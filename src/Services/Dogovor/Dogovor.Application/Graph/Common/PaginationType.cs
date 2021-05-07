using GraphQL.Types;

namespace Dogovor.Application.Graph.Common
{
    public class PaginationType : InputObjectGraphType<Pagination>
    {
        public PaginationType()
        {
            Field(i => i.Skip);
            Field(i => i.Take);
        }
    }
}