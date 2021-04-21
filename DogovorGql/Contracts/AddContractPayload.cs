using System.Collections.Generic;
using PMIS.DogovorGql.Common;
using PMIS.DogovorGql.Data;

namespace PMIS.DogovorGql.Contracts
{
    public class AddContractPayload : ContractPayloadBase
    {
        public AddContractPayload(Contract contract)
            : base(contract)
        {
        }

        public AddContractPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}