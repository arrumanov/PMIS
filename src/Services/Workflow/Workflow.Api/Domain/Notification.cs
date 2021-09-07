using System;

namespace Workflow.Api.Domain
{
    public class Notification
    {
        public Guid Id { get; set; }
        public string Text { get; }

        public string TargetGroup { get; }

        public string TargetUser { get; }

        public bool IsRead { get; private set; }

        public Notification(string text, string targetGroup, string targetUser)
        {
            Id = Guid.NewGuid();
            Text = text;
            TargetGroup = targetGroup;
            TargetUser = targetUser;
            IsRead = false;
        }

        public void MarkAsRead() => this.IsRead = true;

        protected Notification()
        {
        }
    }
}