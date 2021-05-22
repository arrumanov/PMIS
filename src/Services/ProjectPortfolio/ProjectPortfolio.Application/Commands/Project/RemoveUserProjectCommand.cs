using System;
using ProjectPortfolio.Application.MessageHandler;

namespace ProjectPortfolio.Application.Commands.Project
{
    public class RemoveUserProjectCommand : CommandBase<ProjectUserMessage>
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
    }
}