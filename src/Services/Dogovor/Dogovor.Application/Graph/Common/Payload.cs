using System.Collections.Generic;

namespace Dogovor.Application.Graph.Common
{
    public class Payload
    {
        protected Payload(IReadOnlyList<UserError>? errors = null)
        {
            Errors = errors;
        }

        public IReadOnlyList<UserError>? Errors { get; }
    }
}