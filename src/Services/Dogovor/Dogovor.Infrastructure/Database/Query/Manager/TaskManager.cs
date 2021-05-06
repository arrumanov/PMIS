using Dogovor.Infrastructure.Database.Query.Model.Task;

namespace Dogovor.Infrastructure.Database.Query.Manager
{
    public class TaskManager : EntityManager<Task>
    {
        public TaskManager(IManager<Task> manager) : base(manager)
        {
        }
    }
}