using System;

namespace Dogovor.Application.Commands.Task
{
    public class UpdateDeadlineCommand : CommandBase<bool>
    {
        public Guid Id { get; set; }
        public DateTime Deadline { get; set; }
    }
}