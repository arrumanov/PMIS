using System;

namespace Dogovor.Application.Commands.Project
{
    public class RemoveUserProjectCommand : CommandBase<bool>
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
    }
}