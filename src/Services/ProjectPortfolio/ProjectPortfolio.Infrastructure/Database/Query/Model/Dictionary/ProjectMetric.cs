using MongoDB.Bson.Serialization.Attributes;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.Infrastructure.Database.Query.Model.Dictionary
{
    public class ProjectMetric : IQueryModel
    {
        public string Id { get; set; }
        
        [BsonElement("name")]
        public string Name { get; set; }
        
        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("sequence")]
        public string Sequence { get; set; }
    }
}