using GraphQL.Types;
using Model = Dogovor.Infrastructure.Database.Query.Model.Project;

namespace Dogovor.Application.Graph.Project.Types.Query
{
    public class ProjectTaskType : ObjectGraphType<Model.ProjectTask>
    {
        public ProjectTaskType()
        {
            Field(i => i.Id);
            Field(i => i.Description);
            Field(i => i.Status);
            Field(i => i.Responsible);
        }
    }
}