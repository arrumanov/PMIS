using Dogovor.Infrastructure.Database.Query.Model;

namespace Dogovor.Infrastructure.Database.Query.Manager
{
    public class ContractManager : EntityManager<Contract>
    {
        public ContractManager(IManager<Contract> manager) : base(manager)
        {
        }
    }
}