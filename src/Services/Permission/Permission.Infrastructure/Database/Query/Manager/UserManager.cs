using Permission.Infrastructure.Database.Query.Model;

namespace Permission.Infrastructure.Database.Query.Manager
{
    public class UserManager : EntityManager<User>
    {
        public UserManager(IManager<User> manager) : base(manager)
        {
        }
    }
}