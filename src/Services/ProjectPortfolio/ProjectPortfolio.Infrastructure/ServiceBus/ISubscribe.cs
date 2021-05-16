using System.Threading.Tasks;

namespace ProjectPortfolio.Infrastructure.ServiceBus
{
    public interface ISubscribe
    {
        Task HandleMessage(Message message);
    }
}