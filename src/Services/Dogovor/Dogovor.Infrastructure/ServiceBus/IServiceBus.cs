using System.Threading.Tasks;

namespace Dogovor.Infrastructure.ServiceBus
{
    public interface IServiceBus
    {
        Task SendMessage(Message message);
    }
}