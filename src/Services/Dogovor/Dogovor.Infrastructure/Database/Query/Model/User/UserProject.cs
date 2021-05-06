using MongoDB.Bson.Serialization.Attributes;

namespace Dogovor.Infrastructure.Database.Query.Model.User
{
    public class UserProject
    {
        public string Id { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("longDescription")]
        public string LongDescription { get; set; }
    }
}