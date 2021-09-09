using ProjectPortfolio.Infrastructure.Database.Query.Model.Dictionary;

namespace ProjectPortfolio.Infrastructure.Database.Query.Manager
{
    public class DictionaryManager : EntityManager<Dictionary>
    {
        public DictionaryManager(IManager<Dictionary> manager) : base(manager)
        {
        }
    }
}