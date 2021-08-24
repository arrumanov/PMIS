using MongoDB.Bson.Serialization.Attributes;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.Infrastructure.Database.Query.Model.Dictionary
{
    public class Dictionary : IQueryModel
    {
        public string Id { get; set; }

        [BsonElement("dictionaryKey")]
        public string DictionaryKey { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}