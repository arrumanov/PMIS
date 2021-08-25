using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPortfolio.Infrastructure.Database.Query.Model.Project
{
    public class ProjectContragent
    {
        [BsonElement("ContragentId")]
        public string ContragentId { get; set; }    
    }
}