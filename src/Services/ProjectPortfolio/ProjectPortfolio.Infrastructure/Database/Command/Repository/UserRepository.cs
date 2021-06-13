using ProjectPortfolio.Infrastructure.Database.Command.Interfaces;
using ProjectPortfolio.Infrastructure.Database.Command.Model;

namespace ProjectPortfolio.Infrastructure.Database.Command.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ProjectPortfolioContext context) : base(context)
        {
        }

    }
}