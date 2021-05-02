using System;

namespace PMIS.Contracts
{
    public class ProjectSavedMessageAcceptedFromSearchIndex
    {
        public Guid MessageId { get; set; }

        public bool Accepted { get; set; }
    }
}