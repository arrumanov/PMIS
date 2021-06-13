using System.Collections.Generic;
using ProjectPortfolio.Application.Graph.Common;

namespace ProjectPortfolio.Application.Graph.Project.Types
{
    public class AddProjectPayload : ProjectPayloadBase
    {
        public AddProjectPayload(Infrastructure.Database.Query.Model.Project.Project project)
            : base(project)
        {
        }

        public AddProjectPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}