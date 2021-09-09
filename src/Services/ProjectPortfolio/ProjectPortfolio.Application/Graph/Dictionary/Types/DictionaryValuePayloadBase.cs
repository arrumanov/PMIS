using System.Collections.Generic;
using ProjectPortfolio.Application.Graph.Common;

namespace ProjectPortfolio.Application.Graph.Dictionary.Types
{
    public class DictionaryValuePayloadBase : Payload
    {
        public DictionaryValuePayloadBase(Infrastructure.Database.Query.Model.Dictionary.DictionaryValue dictionaryValue)
        {
            DictionaryValue = dictionaryValue;
        }

        public DictionaryValuePayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Infrastructure.Database.Query.Model.Dictionary.DictionaryValue? DictionaryValue { get; }
    }
}