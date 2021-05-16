using ProjectPortfolio.Infrastructure.Database.Query.Model.Project;
using ProjectPortfolio.Infrastructure.Database.Query.Model.Task;

namespace ProjectPortfolio.Application.MessageHandler
{
    public class TaskProjectMessage
    {
        public Project Project { get; set; }
        public Task Task { get; set; }
    }
}