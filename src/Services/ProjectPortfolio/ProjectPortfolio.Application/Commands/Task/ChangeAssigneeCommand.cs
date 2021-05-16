using System;

namespace ProjectPortfolio.Application.Commands.Task
{
    public class ChangeAssigneeCommand : CommandBase<bool>
    {
        public Guid Id { get; set; }
        public Guid NewAssigneeId { get; set; }
    }
}