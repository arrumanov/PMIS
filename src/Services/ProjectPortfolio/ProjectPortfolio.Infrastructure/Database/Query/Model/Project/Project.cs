using System.Collections.Generic;
using ProjectPortfolio.CrossCutting.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using ProjectPortfolio.Infrastructure.Database.Command.Model;

namespace ProjectPortfolio.Infrastructure.Database.Query.Model.Project
{
    public class Project : IQueryModel
    {
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("responsibleDepartmentId")]
        public string ResponsibleDepartmentId { get; set; }

        [BsonElement("category")]
        public DictionaryValue Category { get; set; }

        [BsonElement("type")]
        public DictionaryValue Type { get; set; }

        //[BsonElement("initiator")]
        //public string Initiator { get; set; }

        //[BsonElement("curator")]
        //public string Curator { get; set; }

        //[BsonElement("manager")]
        //public string Manager { get; set; }


        [BsonElement("probability")]
        public DictionaryValue Probability { get; set; }

        [BsonElement("statement")]
        public DictionaryValue Statement { get; set; }

        //[BsonElement("status")]
        //public string Status { get; set; }

        //[BsonElement("creatorId")]
        //public string CreatorId { get; set; }

        [BsonElement("createdDate")]
        public long CreatedDate { get; set; }

        //[BsonElement("objectType")]
        //public string ObjectType { get; set; }



        [BsonElement("departments")]
        public ICollection<ProjectDepartment> ProjectDepartments { get; set; }

        [BsonElement("products")]
        public ICollection<ProjectProduct> ProjectProducts { get; set; }

        [BsonElement("contragents")]
        public ICollection<ProjectContragent> ProjectContragents { get; set; }

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