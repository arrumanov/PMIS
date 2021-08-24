using MongoDB.Bson.Serialization.Attributes;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.Infrastructure.Database.Query.Model.CustomField
{
    public class CustomFieldValue : IQueryModel
    {
        public string Id { get; set; }

        [BsonElement("fieldKey")]
        public string FieldKey { get; set; }

        [BsonElement("objectId")]
        public string ObjectId { get; set; }

        [BsonElement("version")]
        public string Version { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("dateTime")]
        public string DateTime { get; set; }

        [BsonElement("bool")]
        public string Bool { get; set; }

        //необходим для реализации наследования
        [BsonElement("discriminator")]
        public string Discriminator { get; set; }
    }
}