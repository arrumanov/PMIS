using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPortfolio.Infrastructure.Database.Query.Model.Project
{
    public class ProjectContragent
    {
        [BsonElement("contragentId")]
        public string ContragentId { get; set; }    
    }
}