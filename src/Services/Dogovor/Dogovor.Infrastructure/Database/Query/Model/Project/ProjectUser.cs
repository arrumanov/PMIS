using MongoDB.Bson.Serialization.Attributes;

namespace Dogovor.Infrastructure.Database.Query.Model.Project
{
    public class ProjectUser
    {
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}