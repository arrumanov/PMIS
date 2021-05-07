using Dogovor.Application.Graph.Common;
using GraphQL.Types;

namespace Dogovor.Application.Graph.Task.Types.Input
{
    public class TaskFilterType : InputObjectGraphType
    {
        public TaskFilterType()
        {
            Field<FilterType>("description");
            Field<DateFilterType>("deadLine");
            Field<FilterType>("assignee");
            Field<FilterType>("reporter");
            Field<EnumFilterType>("status");
        }
    }
}
