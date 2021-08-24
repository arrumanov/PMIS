using MongoDB.Bson.Serialization.Attributes;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.Infrastructure.Database.Query.Model.Dictionary
{
    public class ProjectRisk : IQueryModel
    {
        public string Id { get; set; }

        [BsonElement("productId")]
        public string ProductId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("conditionOffensive")]
        public string ConditionOffensive { get; set; }

        [BsonElement("probabilityOffensive")]
        public string ProbabilityOffensive { get; set; }

        [BsonElement("degreeEffect")]
        public string DegreeEffect { get; set; }

        [BsonElement("methodOfResponse")]
        public string MethodOfResponse { get; set; }
    }
}