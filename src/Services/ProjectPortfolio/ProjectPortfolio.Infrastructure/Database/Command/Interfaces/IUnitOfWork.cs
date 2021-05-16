using Thread = System.Threading.Tasks;

namespace ProjectPortfolio.Infrastructure.Database.Command.Interfaces
{
    public interface IUnitOfWork
    {
        Thread.Task Commit();
    }
}