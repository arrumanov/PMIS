using Thread = System.Threading.Tasks;

namespace Permission.Infrastructure.Database.Command.Interfaces
{
    public interface IUnitOfWork
    {
        Thread.Task Commit();
    }
}