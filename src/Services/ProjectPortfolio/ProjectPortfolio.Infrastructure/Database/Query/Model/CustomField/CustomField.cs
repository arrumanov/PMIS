using MongoDB.Bson.Serialization.Attributes;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.Infrastructure.Database.Query.Model.CustomField
{
    public class CustomField : IQueryModel
    {
        public string Id { get; set; }

        [BsonElement("fieldKey")]
        public string FieldKey { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("dictonaryKey")]
        public string DictonaryKey { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        //необходим для реализации наследования
        [BsonElement("discriminator")]
        public string Discriminator { get; set; }
    }
}