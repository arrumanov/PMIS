using System;

namespace Dogovor.Application.Commands.Task
{
    public class ChangeAssigneeCommand : CommandBase<bool>
    {
        public Guid Id { get; set; }
        public Guid NewAssigneeId { get; set; }
    }
}