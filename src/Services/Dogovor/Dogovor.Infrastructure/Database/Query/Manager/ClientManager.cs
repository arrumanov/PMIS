using Dogovor.Infrastructure.Database.Query.Model.Client;

namespace Dogovor.Infrastructure.Database.Query.Manager
{
    public class ClientManager : EntityManager<Client>
    {
        public ClientManager(IManager<Client> manager) : base(manager)
        {
        }
    }
}