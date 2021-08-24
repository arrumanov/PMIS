using System;

namespace ProjectPortfolio.Application.Commands.Project
{
    public class AddProjectCommand : CommandBase<Infrastructure.Database.Query.Model.Project.Project>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid ContragentId { get; set; }
        public Guid ProductId { get; set; }
    }
}