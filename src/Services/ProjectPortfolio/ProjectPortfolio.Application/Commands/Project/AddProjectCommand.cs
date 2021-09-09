using System;
using System.Collections.Generic;

namespace ProjectPortfolio.Application.Commands.Project
{
    public class AddProjectCommand : CommandBase<Infrastructure.Database.Query.Model.Project.Project>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid TypeId { get; set; }
        public Guid ResponsibleDepartmentId { get; set; }
        public Guid InitiatorId { get; set; }
        public Guid CuratorId { get; set; }
        public Guid ManagerId { get; set; }
        public List<Guid> DepartmentIds { get; set; }
        public List<Guid> ContragentIds { get; set; }
        public List<Guid> ProductIds { get; set; }
    }
}