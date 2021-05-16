using System.Collections.Generic;
using ProjectPortfolio.CrossCutting.Interfaces;
using ProjectPortfolio.Infrastructure.Database.Command.Model;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPortfolio.Infrastructure.Database.Query.Model.User
{
    public class User : IQueryModel
    {
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("projects")]
        public IEnumerable<UserProject> Projects { get; set; }
    }
}