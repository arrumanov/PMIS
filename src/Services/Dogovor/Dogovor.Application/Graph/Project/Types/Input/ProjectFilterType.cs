using Dogovor.Application.Graph.Common;
using GraphQL.Types;

namespace Dogovor.Application.Graph.Project.Types.Input
{
    public class ProjectFilterType : InputObjectGraphType
    {
        public ProjectFilterType()
        {
            Field<FilterType>("description");
            Field<FilterType>("longDescription");
        }
    }
}