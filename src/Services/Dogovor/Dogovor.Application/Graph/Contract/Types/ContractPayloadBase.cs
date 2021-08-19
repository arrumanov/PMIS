﻿using System.Collections.Generic;
using Dogovor.Application.Graph.Common;

namespace Dogovor.Application.Graph.Contract.Types
{
    public class ContragentPayloadBase : Payload
    {
        public ContragentPayloadBase(Infrastructure.Database.Query.Model.Contract contract)
        {
            Contract = contract;
        }

        public ContragentPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Infrastructure.Database.Query.Model.Contract? Contract { get; }
    }
}