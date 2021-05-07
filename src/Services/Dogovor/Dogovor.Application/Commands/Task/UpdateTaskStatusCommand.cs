using System;
using Dogovor.CrossCutting;

namespace Dogovor.Application.Commands.Task
{
    public class UpdateTaskStatusCommand : CommandBase<bool>
    {
        public Guid Id { get; set; }
        public TaskStatusEnum Status { get; set; }
    }
}