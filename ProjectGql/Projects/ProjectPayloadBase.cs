using System.Collections.Generic;
using PMIS.ProjectGql.Common;
using PMIS.ProjectGql.Data;

namespace PMIS.ProjectGql.Projects
{
    public class ProjectPayloadBase : Payload
    {
        public ProjectPayloadBase(Project project)
        {
            Project = project;
        }

        public ProjectPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Project? Project { get; }
    }
}