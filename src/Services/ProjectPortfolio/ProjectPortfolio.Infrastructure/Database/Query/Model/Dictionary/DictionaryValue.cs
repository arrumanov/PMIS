using MongoDB.Bson.Serialization.Attributes;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.Infrastructure.Database.Query.Model.Dictionary
{
    public class DictionaryValue : IQueryModel
    {
        public string Id { get; set; }

        [BsonElement("dictionaryKey")]
        public string DictionaryKey { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("isActive")]
        public string IsActive { get; set; }

        [BsonElement("code")]
        public string Code { get; set; }

        [BsonElement("sequence")]
        public string Sequence { get; set; }
    }
}