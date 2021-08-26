using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPortfolio.Infrastructure.Database.Query.Model.Project
{
    public class ProjectDepartment
    {
        [BsonElement("departmentId")]
        public string DepartmentId { get; set; }    
    }
}