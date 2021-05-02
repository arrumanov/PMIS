using System;

namespace PMIS.Contracts
{
    public interface IProjectDetailedMessage
    {
        Guid MessageId { get; set; }
        Project Project { get; set; }

        DateTime CreationDate { get; set; }
    }
}