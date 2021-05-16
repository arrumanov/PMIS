using ProjectPortfolio.Infrastructure.Database.Query.Model.User;

namespace ProjectPortfolio.Infrastructure.Database.Query.Manager
{
    public class UserManager : EntityManager<User>
    {
        public UserManager(IManager<User> manager) : base(manager)
        {
        }
    }
}