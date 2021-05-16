using Thread = System.Threading.Tasks;
using ProjectPortfolio.Infrastructure.Database.Command.Model;

namespace ProjectPortfolio.Infrastructure.Database.Command.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Thread.Task AddUser(UserProject userProject);
        Thread.Task RemoveUser(UserProject userProject);
    }
}