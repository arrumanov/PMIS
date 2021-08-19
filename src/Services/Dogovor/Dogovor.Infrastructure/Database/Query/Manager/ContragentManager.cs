using Dogovor.Infrastructure.Database.Query.Model;

namespace Dogovor.Infrastructure.Database.Query.Manager
{
    public class ContragentManager : EntityManager<Contragent>
    {
        public ContragentManager(IManager<Contragent> manager) : base(manager)
        {
        }
    }
}