using Dogovor.Infrastructure.Database.Command.Interfaces;
using Dogovor.Infrastructure.Database.Command.Model;

namespace Dogovor.Infrastructure.Database.Command.Repository
{
    public class ContragentRepository : Repository<Contragent>, IContragentRepository
    {
        public ContragentRepository(GraphContext context) : base(context)
        {
        }
    }
}