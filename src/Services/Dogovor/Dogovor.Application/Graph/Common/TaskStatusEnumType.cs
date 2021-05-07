using Dogovor.CrossCutting;
using GraphQL.Types;

namespace Dogovor.Application.Graph.Common
{
    public class TaskStatusEnumType : EnumerationGraphType<TaskStatusEnum>
    {
        public TaskStatusEnumType()
        {

        }
    }
}