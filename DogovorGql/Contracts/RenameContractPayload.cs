using System.Collections.Generic;
using PMIS.DogovorGql.Common;
using PMIS.DogovorGql.Data;

namespace PMIS.DogovorGql.Contracts
{
    public class RenameContractPayload : ContractPayloadBase
    {
        public RenameContractPayload(Contract contract)
            : base(contract)
        {
        }

        public RenameContractPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}