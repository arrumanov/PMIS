using System.Collections.Generic;
using ProjectPortfolio.Application.Graph.Common;

namespace ProjectPortfolio.Application.Graph.Project
{
    public class RenameProjectPayload : ProjectPayloadBase
    {
        public RenameProjectPayload(Infrastructure.Database.Query.Model.Project.Project project)
            : base(project)
        {
        }

        public RenameProjectPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}