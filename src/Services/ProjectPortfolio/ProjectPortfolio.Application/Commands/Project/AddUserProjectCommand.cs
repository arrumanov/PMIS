using System;
using ProjectPortfolio.Application.MessageHandler;

namespace ProjectPortfolio.Application.Commands.Project
{
    public class AddUserProjectCommand : CommandBase<ProjectUserMessage>
    {
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
    }
}