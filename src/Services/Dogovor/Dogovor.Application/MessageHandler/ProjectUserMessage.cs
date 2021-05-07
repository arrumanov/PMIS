using Dogovor.Infrastructure.Database.Query.Model.Project;
using Dogovor.Infrastructure.Database.Query.Model.User;

namespace Dogovor.Application.MessageHandler
{
    public class ProjectUserMessage
    {
        public Project Project { get; set; }
        public User User { get; set; }
    }
}