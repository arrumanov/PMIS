using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPortfolio.Infrastructure.Database.Query.Model.Project
{
    public class ProjectProduct
    {
        [BsonElement("productId")]
        public string ProductId { get; set; }    
    }
}