using System.Collections.Generic;
using Dogovor.Application.Graph.Common;

namespace Dogovor.Application.Graph.Contragent.Types
{
    public class ContragentPayloadBase : Payload
    {
        public ContragentPayloadBase(Infrastructure.Database.Query.Model.Contragent contragent)
        {
            Contragent = contragent;
        }

        public ContragentPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Infrastructure.Database.Query.Model.Contragent? Contragent { get; }
    }
}