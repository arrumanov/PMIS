using System;
using HotChocolate.Types.Relay;

namespace PMIS.DogovorGql.Contracts
{
    public record RenameContractInput([ID /*(nameof(Contract))*/] Guid Id, string Name);
}