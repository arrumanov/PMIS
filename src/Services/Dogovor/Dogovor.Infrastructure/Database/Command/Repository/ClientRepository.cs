using Dogovor.Infrastructure.Database.Command.Interfaces;
using Dogovor.Infrastructure.Database.Query.Model.Client;

namespace Dogovor.Infrastructure.Database.Command.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(GraphContext context) : base(context)
        {
        }
    }
}