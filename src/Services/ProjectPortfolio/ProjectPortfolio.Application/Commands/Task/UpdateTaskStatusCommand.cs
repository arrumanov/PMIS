using System;
using ProjectPortfolio.CrossCutting;

namespace ProjectPortfolio.Application.Commands.Task
{
    public class UpdateTaskStatusCommand : CommandBase<bool>
    {
        public Guid Id { get; set; }
        public TaskStatusEnum Status { get; set; }
    }
}