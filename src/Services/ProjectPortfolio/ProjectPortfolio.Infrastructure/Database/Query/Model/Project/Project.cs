using System;
using System.Collections.Generic;
using ProjectPortfolio.CrossCutting.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPortfolio.Infrastructure.Database.Query.Model.Project
{
    public class Project : IQueryModel
    {
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        //[BsonElement("category")]
        //public DictionaryValue Category { get; set; }

        //[BsonElement("type")]
        //public DictionaryValue Type { get; set; }

        //[BsonElement("initiator")]
        //public string Initiator { get; set; }

        //[BsonElement("curator")]
        //public string Curator { get; set; }

        //[BsonElement("manager")]
        //public string Manager { get; set; }

        [BsonElement("responsibleDepartmentId")]
        public string ResponsibleDepartmentId { get; set; }

        [BsonElement("departmentIds")]
        public ICollection<string> DepartmentIds { get; set; }

        //[BsonElement("contragentIds")]
        //public ICollection<string> ContragentIds { get; set; }

        [BsonElement("productIds")]
        public ICollection<string> ProductIds { get; set; }

        //TODO: костыль, в будущем нужно найти решение лучше
        [BsonIgnore]
        public string DepartmentIdsStr { get; set; }

        //[BsonIgnore]
        //public string ContragentIdsStr { get; set; }

        [BsonIgnore]
        public string ProductIdsStr { get; set; }
        //:TODO

        //[BsonElement("probability")]
        //public string Probability { get; set; }

        //[BsonElement("statement")]
        //public string Statement { get; set; }

        //[BsonElement("status")]
        //public string Status { get; set; }

        //[BsonElement("creatorId")]
        //public string CreatorId { get; set; }

        //[BsonElement("creationDate")]
        //public string CreationDate { get; set; }

        //[BsonElement("objectType")]
        //public string ObjectType { get; set; }



        [BsonElement("contragents")]
        public List<ProjectContragent> ProjectContragents { get; set; }

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