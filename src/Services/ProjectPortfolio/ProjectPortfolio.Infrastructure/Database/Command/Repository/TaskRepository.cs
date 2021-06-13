using ProjectPortfolio.Infrastructure.Database.Command.Interfaces;
using ProjectPortfolio.Infrastructure.Database.Command.Model;

namespace ProjectPortfolio.Infrastructure.Database.Command.Repository
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public TaskRepository(ProjectPortfolioContext context) : base(context)
        {
        }

    }
}