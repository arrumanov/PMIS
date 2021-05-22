using System;

namespace ProjectPortfolio.Application.Commands.Project
{
    public class UpdateProjectInfoCommand : CommandBase<Infrastructure.Database.Query.Model.Project.Project>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
    }
}