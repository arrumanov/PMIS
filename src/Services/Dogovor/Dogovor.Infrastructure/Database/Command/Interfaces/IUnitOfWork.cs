using Thread = System.Threading.Tasks;

namespace Dogovor.Infrastructure.Database.Command.Interfaces
{
    public interface IUnitOfWork
    {
        Thread.Task Commit();
    }
}