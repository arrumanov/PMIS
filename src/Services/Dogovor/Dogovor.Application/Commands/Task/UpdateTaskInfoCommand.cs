using System;

namespace Dogovor.Application.Commands.Task
{
    public class UpdateTaskInfoCommand : CommandBase<bool>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }

    }
}