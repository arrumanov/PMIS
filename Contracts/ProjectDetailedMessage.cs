using System;

namespace PMIS.Contracts
{
    public class ProjectDetailedMessage : IProjectDetailedMessage
    {
        public Guid MessageId { get; set; }
        public Project Project { get; set; }
        public DateTime CreationDate { get; set; }

    }
}