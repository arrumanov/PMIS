using GraphQL.Types;

namespace Dogovor.Application.Graph.Common
{
    public class FilterType : InputObjectGraphType<Filter>
    {
        public FilterType()
        {
            Field(f => f.Operation);
            Field(f => f.Value);
        }
    }
}