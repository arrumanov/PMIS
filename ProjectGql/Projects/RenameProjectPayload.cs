using System.Collections.Generic;
using PMIS.ProjectGql.Common;
using PMIS.ProjectGql.Data;

namespace PMIS.ProjectGql.Projects
{
    public class RenameProjectPayload : ProjectPayloadBase
    {
        public RenameProjectPayload(Project project)
            : base(project)
        {
        }

        public RenameProjectPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}