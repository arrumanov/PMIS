using System;
using HotChocolate.Types.Relay;
using PMIS.ProjectGql.Data;

namespace PMIS.ProjectGql.Projects
{
    public record RenameProjectInput([ID/*(nameof(Project))*/] Guid Id, string Name);
}