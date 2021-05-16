using ProjectPortfolio.Infrastructure.Database.Query.Model.Project;
using ProjectPortfolio.Infrastructure.Database.Query.Model.User;

namespace ProjectPortfolio.Application.MessageHandler
{
    public class ProjectUserMessage
    {
        public Project Project { get; set; }
        public User User { get; set; }
    }
}