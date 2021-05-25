using Dogovor.Infrastructure.Database.Command.Interfaces;
using Dogovor.Infrastructure.Database.Command.Model;

namespace Dogovor.Infrastructure.Database.Command.Repository
{
    public class ContractRepository : Repository<Contract>, IContractRepository
    {
        public ContractRepository(GraphContext context) : base(context)
        {
        }
    }
}