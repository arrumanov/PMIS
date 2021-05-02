using System;

namespace PMIS.Contracts
{
    public class ProjectChangedMessage : IProjectChangedMessage
    {
        public Guid MessageId { get; set; }
        public ProjectDto Project { get; set; }
        public DateTime CreationDate { get; set; }
    }
}