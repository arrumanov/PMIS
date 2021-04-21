using System.Collections.Generic;
using PMIS.DogovorGql.Common;
using PMIS.DogovorGql.Data;

namespace PMIS.DogovorGql.Contracts
{
    public class ContractPayloadBase : Payload
    {
        public ContractPayloadBase(Contract contract)
        {
            Contract = contract;
        }

        public ContractPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Contract? Contract { get; }
    }
}