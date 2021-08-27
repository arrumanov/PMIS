using System.Threading.Tasks;

namespace Permission.Infrastructure.ServiceBus
{
    public interface ISubscribe
    {
        Task HandleMessage(Message message);
    }
}