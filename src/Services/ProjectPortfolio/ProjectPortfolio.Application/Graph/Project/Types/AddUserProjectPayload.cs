using System.Collections.Generic;
using ProjectPortfolio.Application.Graph.Common;

namespace ProjectPortfolio.Application.Graph.Project.Types
{
    public class AddUserProjectPayload : ProjectPayloadBase
    {
        public AddUserProjectPayload(Infrastructure.Database.Query.Model.Project.Project project, Infrastructure.Database.Query.Model.User.User user)
            : base(project)
        {
        }

        public AddUserProjectPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Infrastructure.Database.Query.Model.User.User? User { get; }
    }
}