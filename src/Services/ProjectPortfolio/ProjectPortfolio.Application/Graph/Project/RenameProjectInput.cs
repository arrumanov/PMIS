using System;
using HotChocolate.Types.Relay;

namespace ProjectPortfolio.Application.Graph.Project
{
    public record RenameProjectInput([ID/*(nameof(Project))*/] Guid Id, string Name);
}