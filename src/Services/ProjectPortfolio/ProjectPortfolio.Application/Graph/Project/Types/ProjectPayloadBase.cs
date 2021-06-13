using System.Collections.Generic;
using ProjectPortfolio.Application.Graph.Common;

namespace ProjectPortfolio.Application.Graph.Project.Types
{
    public class ProjectPayloadBase : Payload
    {
        public ProjectPayloadBase(Infrastructure.Database.Query.Model.Project.Project project)
        {
            Project = project;
        }

        public ProjectPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Infrastructure.Database.Query.Model.Project.Project? Project { get; }
    }
}