using System.Collections.Generic;
using ProjectPortfolio.Application.Graph.Common;

namespace ProjectPortfolio.Application.Graph.Dictionary.Types
{
    public class AddDictionaryPayload : DictionaryPayloadBase
    {
        public AddDictionaryPayload(Infrastructure.Database.Query.Model.Dictionary.Dictionary dictionary) : base(dictionary)
        {
        }

        public AddDictionaryPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}