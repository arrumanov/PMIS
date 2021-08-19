using System.Collections.Generic;
using Dogovor.Application.Graph.Common;

namespace Dogovor.Application.Graph.Contract.Types
{
    public class AddContractPayload : ContragentPayloadBase
    {
        public AddContractPayload(Infrastructure.Database.Query.Model.Contract contract)
            : base(contract)
        {
        }

        public AddContractPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}