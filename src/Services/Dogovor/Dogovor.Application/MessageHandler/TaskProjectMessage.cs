using Dogovor.Infrastructure.Database.Query.Model.Project;
using Dogovor.Infrastructure.Database.Query.Model.Task;

namespace Dogovor.Application.MessageHandler
{
    public class TaskProjectMessage
    {
        public Project Project { get; set; }
        public Task Task { get; set; }
    }
}