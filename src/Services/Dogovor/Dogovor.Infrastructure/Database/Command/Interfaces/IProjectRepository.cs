using Thread = System.Threading.Tasks;
using Dogovor.Infrastructure.Database.Command.Model;

namespace Dogovor.Infrastructure.Database.Command.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Thread.Task AddUser(UserProject userProject);
        Thread.Task RemoveUser(UserProject userProject);
    }
}