using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPortfolio.Infrastructure.Database.Query.Model.Task
{
    public class TaskProject
    {
        public string Id { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }
    }
}