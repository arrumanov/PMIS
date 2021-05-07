using System;

namespace Dogovor.Application.Commands.Task
{
    public class RemoveTaskCommand : CommandBase<bool>
    {
        public Guid Id { get; set; }
    }
}