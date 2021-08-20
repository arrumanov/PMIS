using System.Collections.Generic;
using Dogovor.Application.Graph.Common;

namespace Dogovor.Application.Graph.User.Types
{
    public class UserPayloadBase : Payload
    {
        public UserPayloadBase(Infrastructure.Database.Query.Model.User user)
        {
            User = user;
        }

        public UserPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Infrastructure.Database.Query.Model.User? User { get; }
    }
}