using System;

namespace Workflow.Api.Domain
{
    public class Project
    {
        public Guid Id { get; }
        public ProjectStatus Status { get; protected set; }
        public string ProcessInstanceId { get; private set; }

        public Project(string name)
        {
            Id = Guid.NewGuid();
            Status = ProjectStatus.New;
            ProcessInstanceId = null;
        }

        protected Project()
        {
        }

        public void AssociateWithProcessInstance(string processInstanceId)
        {
            this.ProcessInstanceId = processInstanceId;
        }

        public void Prepare()
        {
            Status = ProjectStatus.Prepared;
        }

        public void Close()
        {
            Status = ProjectStatus.Closed;
        }

        public void Accept()
        {
            Status = ProjectStatus.Accepted;
        }

        public void Reject()
        {
            Status = ProjectStatus.Rejected;
        }

        public void Cancel()
        {
            Status = ProjectStatus.Cancelled;
        }

        public void CreateInJira()
        {
            Status = ProjectStatus.CreatingInJira;
        }

        public void NotCreatedInJira()
        {
            Status = ProjectStatus.Closed;
        }

        public void MarkProjectCreatedInJira()
        {
            Status = ProjectStatus.ProjectIsMarkedAsCreated;
        }

        public void ProjectCreatedInJira()
        {
            Status = ProjectStatus.ProjectCreatedInJira;
        }
    }

    public enum ProjectStatus
    {
        New,
        Prepared,
        Closed,
        CreatingInJira,
        NotCreatedInJira,
        ProjectCreatedInJira,
        ProjectIsMarkedAsCreated,
        Accepted,
        Rejected,
        Cancelled
    }
}