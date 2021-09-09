using ProjectPortfolio.Infrastructure.Database.Query.Model.Dictionary;

namespace ProjectPortfolio.Infrastructure.Database.Query.Manager
{
    public class DictionaryValueManager : EntityManager<DictionaryValue>
    {
        public DictionaryValueManager(IManager<DictionaryValue> manager) : base(manager)
        {
        }
    }
}