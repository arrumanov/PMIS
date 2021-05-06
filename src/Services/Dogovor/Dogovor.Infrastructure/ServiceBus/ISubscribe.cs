using System.Threading.Tasks;

namespace Dogovor.Infrastructure.ServiceBus
{
    public interface ISubscribe
    {
        Task HandleMessage(Message message);
    }
}