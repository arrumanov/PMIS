using System.Collections.Generic;
using Dogovor.CrossCutting.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace Dogovor.Infrastructure.Database.Query.Model.Project
{
    public class Project : IQueryModel
    {
        public string Id { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("longDescription")]
        public string LongDescription { get; set; }

        [BsonElement("tasks")]
        public ICollection<ProjectTask> Tasks { get; set; }

        [BsonElement("participants")]
        public ICollection<ProjectUser> Participants { get; set; }

        [BsonElement("finishedCount")]
        public int FinishedCount { get; set; }

        [BsonElement("unfinishedCount")]
        public int UnfinishedCount { get; set; }
    }
}