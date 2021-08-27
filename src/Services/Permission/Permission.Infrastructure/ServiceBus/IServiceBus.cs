using System.Threading.Tasks;

namespace Permission.Infrastructure.ServiceBus
{
    public interface IServiceBus
    {
        Task SendMessage(Message message);
    }
}