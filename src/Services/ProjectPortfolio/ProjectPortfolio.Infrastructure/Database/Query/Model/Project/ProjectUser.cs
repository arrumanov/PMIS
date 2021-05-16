using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPortfolio.Infrastructure.Database.Query.Model.Project
{
    public class ProjectUser
    {
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}