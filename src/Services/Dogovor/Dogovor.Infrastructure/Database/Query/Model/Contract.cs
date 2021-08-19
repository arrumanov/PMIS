using System;
using Dogovor.CrossCutting.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace Dogovor.Infrastructure.Database.Query.Model
{
    public class Contract : IQueryModel
    {
        public string Id { get; set; }

        [BsonElement("name")]
        public string Number { get; set; }

        //[BsonElement("createdDate")]
        //public DateTime CreatedDate { get; set; }
    }
}