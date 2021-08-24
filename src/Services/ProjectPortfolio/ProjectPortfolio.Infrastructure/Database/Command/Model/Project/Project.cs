using System;
using System.Collections.Generic;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.Infrastructure.Database.Command.Model
{
    public class Project : IModel
    {
        public Project()
        {
            UserProjects = new List<UserProject>();
            Tasks = new List<Task>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        //public Guid CategoryId { get; set; }

        //public Guid TypeId { get; set; }

        //public Guid InitiatorId { get; set; }
        
        //public Guid CuratorId { get; set; }
        
        //public Guid ManagerId { get; set; }
        
        //public Guid ResponsibleDepartmentId { get; set; }
        
        public Guid DepartmentId { get; set; }

        public Guid ContragentId { get; set; }

        public Guid ProductId { get; set; }

        //public Guid ProbabilityId { get; set; }

        //public Guid StatementId { get; set; }

        //public Guid StatusId { get; set; }

        //public Guid CreatorId { get; set; }

        //public DateTime CreationDate { get; set; }

        //public Guid ObjectTypeId { get; set; }




        public virtual ICollection<UserProject> UserProjects { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}