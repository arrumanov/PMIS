using ProjectPortfolio.Infrastructure.Database.Query.Model.Task;

namespace ProjectPortfolio.Infrastructure.Database.Query.Manager
{
    public class TaskManager : EntityManager<Task>
    {
        public TaskManager(IManager<Task> manager) : base(manager)
        {
        }
    }
}