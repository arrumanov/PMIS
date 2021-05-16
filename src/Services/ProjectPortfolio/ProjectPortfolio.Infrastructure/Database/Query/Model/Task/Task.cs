﻿using ProjectPortfolio.CrossCutting.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPortfolio.Infrastructure.Database.Query.Model.Task
{
    public class Task : IQueryModel
    {
        public string Id { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("longDescription")]
        public string LongDescription { get; set; }

        [BsonElement("createdDate")]
        public long CreatedDate { get; set; }

        [BsonElement("deadLine")]
        public long DeadLine { get; set; }

        [BsonElement("assignee")]
        public TaskUser Assignee { get; set; }

        [BsonElement("reporter")]
        public TaskUser Reporter { get; set; }

        [BsonElement("project")]
        public TaskProject Project { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }
    }
}