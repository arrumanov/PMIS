using System.Collections.Generic;
using Dogovor.Application.Graph.Common;

namespace Dogovor.Application.Graph.Contract.Types
{
    public class AddContractPayload : ContractPayloadBase
    {
        public AddContractPayload(Infrastructure.Database.Query.Model.Contract.Contract contract)
            : base(contract)
        {
        }

        public AddContractPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}