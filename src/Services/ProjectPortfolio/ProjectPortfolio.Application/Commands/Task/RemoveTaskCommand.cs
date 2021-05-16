using System;

namespace ProjectPortfolio.Application.Commands.Task
{
    public class RemoveTaskCommand : CommandBase<bool>
    {
        public Guid Id { get; set; }
    }
}