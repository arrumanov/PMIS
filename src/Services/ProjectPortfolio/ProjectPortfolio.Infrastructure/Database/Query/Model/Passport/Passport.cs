using System;
using MongoDB.Bson.Serialization.Attributes;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.Infrastructure.Database.Query.Model.Passport
{
    public class Passport : IQueryModel
    {
        public string Id { get; set; }

        [BsonElement("projectId")]
        public string ProjectId { get; set; }

        [BsonElement("version")]
        public string Version { get; set; }

        [BsonElement("creatorId")]
        public string CreatorId { get; set; }

        [BsonElement("creationDate")]
        public DateTime CreationDate { get; set; }
    }
}