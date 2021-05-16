using System.Threading.Tasks;

namespace ProjectPortfolio.Infrastructure.ServiceBus
{
    public interface IServiceBus
    {
        Task SendMessage(Message message);
    }
}