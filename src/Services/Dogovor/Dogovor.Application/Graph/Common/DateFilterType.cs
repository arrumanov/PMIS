using GraphQL.Types;

namespace Dogovor.Application.Graph.Common
{
    public class DateFilterType : InputObjectGraphType<Filter>
    {
        public DateFilterType()
        {
            Field(f => f.Operation);
            Field(f => f.Value);
        }
    }
}