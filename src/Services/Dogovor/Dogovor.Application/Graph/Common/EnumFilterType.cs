using GraphQL.Types;

namespace Dogovor.Application.Graph.Common
{
    public class EnumFilterType : InputObjectGraphType<Filter>
    {
        public EnumFilterType()
        {
            Field(f => f.Operation);
            Field<TaskStatusEnumType>().Name("value");
        }
    }
}