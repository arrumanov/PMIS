using MongoDB.Bson.Serialization.Attributes;
using Permission.CrossCutting.Interfaces;

namespace Permission.Infrastructure.Database.Query.Model
{
    public class User : IQueryModel
    {
        public string Id { get; set; }

        [BsonElement("smallName")]
        public string SmallName { get; set; }
    }
}