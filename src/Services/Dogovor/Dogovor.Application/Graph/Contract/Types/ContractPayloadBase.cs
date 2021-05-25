using System.Collections.Generic;
using Dogovor.Application.Graph.Common;

namespace Dogovor.Application.Graph.Contract.Types
{
    public class ContractPayloadBase : Payload
    {
        public ContractPayloadBase(Infrastructure.Database.Query.Model.Contract.Contract contract)
        {
            Contract = contract;
        }

        public ContractPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Infrastructure.Database.Query.Model.Contract.Contract? Contract { get; }
    }
}