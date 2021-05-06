using Dogovor.Infrastructure.Database.Query.Model.User;

namespace Dogovor.Infrastructure.Database.Query.Manager
{
    public class UserManager : EntityManager<User>
    {
        public UserManager(IManager<User> manager) : base(manager)
        {
        }
    }
}