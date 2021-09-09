using System.Collections.Generic;
using ProjectPortfolio.Application.Graph.Common;
using ProjectPortfolio.Infrastructure.Database.Query.Model.Dictionary;

namespace ProjectPortfolio.Application.Graph.Dictionary.Types
{
    public class AddDictionaryValuePayload : DictionaryValuePayloadBase
    {
        public AddDictionaryValuePayload(DictionaryValue dictionaryValue) : base(dictionaryValue)
        {
        }

        public AddDictionaryValuePayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}