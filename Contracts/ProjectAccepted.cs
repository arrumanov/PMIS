using System;

namespace PMIS.Contracts
{
    public class ProjectAccepted
    {
        public Guid MessageId { get; set; }

        public bool Accepted { get; set; }
    }
}