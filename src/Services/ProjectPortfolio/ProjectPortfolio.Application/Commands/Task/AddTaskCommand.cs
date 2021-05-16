using System;

namespace ProjectPortfolio.Application.Commands.Task
{
    public class AddTaskCommand : CommandBase<bool>
    {
        public Guid ProjectId { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public DateTime DeadLine { get; set; }
        public Guid AssigneeId { get; set; }
        public Guid ReporterId { get; set; }
    }
}