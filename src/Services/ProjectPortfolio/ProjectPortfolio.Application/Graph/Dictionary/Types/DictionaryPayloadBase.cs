using System.Collections.Generic;
using ProjectPortfolio.Application.Graph.Common;

namespace ProjectPortfolio.Application.Graph.Dictionary.Types
{
    public class DictionaryPayloadBase : Payload
    {
        public DictionaryPayloadBase(Infrastructure.Database.Query.Model.Dictionary.Dictionary dictionary)
        {
            Dictionary = dictionary;
        }

        public DictionaryPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Infrastructure.Database.Query.Model.Dictionary.Dictionary? Dictionary { get; }
    }
}