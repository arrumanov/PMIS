using System;

namespace PMIS.Contracts
{
    public class ProjectSavedMessageAcceptedFromDWH
    {
        public Guid MessageId { get; set; }

        public bool Accepted { get; set; }
    }
}