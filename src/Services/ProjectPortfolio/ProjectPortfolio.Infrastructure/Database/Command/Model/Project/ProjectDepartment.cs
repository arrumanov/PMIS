using System;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.Infrastructure.Database.Command.Model
{
    public class ProjectDepartment : IModel
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public Guid DepartmentId { get; set; }
    }
}