using GraphQL.Types;
using Model = Dogovor.Infrastructure.Database.Query.Model.Project;

namespace Dogovor.Application.Graph.Project.Types.Query
{
    public class ProjectUserType : ObjectGraphType<Model.ProjectUser>
    {
        public ProjectUserType()
        {
            Field(i => i.Id);
            Field(i => i.Name);
        }
    }
}