using System.Collections.Generic;
using PMIS.ProjectGql.Common;
using PMIS.ProjectGql.Data;

namespace PMIS.ProjectGql.Projects
{
    public class AddProjectPayload : ProjectPayloadBase
    {
        public AddProjectPayload(Project project)
            : base(project)
        {
        }

        public AddProjectPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}