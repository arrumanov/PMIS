﻿using MongoDB.Bson.Serialization.Attributes;

namespace Dogovor.Infrastructure.Database.Query.Model.Task
{
    public class TaskUser
    {
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}