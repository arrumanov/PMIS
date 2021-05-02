using System;

namespace PMIS.Contracts
{
    public interface IProjectChangedMessage
    {
        Guid MessageId { get; set; }
        ProjectDto Project { get; set; }
        DateTime CreationDate { get; set; }
    }
}