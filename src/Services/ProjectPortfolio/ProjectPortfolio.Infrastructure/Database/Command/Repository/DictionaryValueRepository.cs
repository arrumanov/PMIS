using ProjectPortfolio.Infrastructure.Database.Command.Interfaces;
using ProjectPortfolio.Infrastructure.Database.Command.Model;

namespace ProjectPortfolio.Infrastructure.Database.Command.Repository
{
    public class DictionaryValueRepository : Repository<DictionaryValue>, IDictionaryValueRepository
    {
        public DictionaryValueRepository(ProjectPortfolioContext context) : base(context)
        {
        }
    }
}