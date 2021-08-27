using Permission.Infrastructure.Database.Command.Interfaces;
using Permission.Infrastructure.Database.Command.Model;

namespace Permission.Infrastructure.Database.Command.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(PermissionContext context) : base(context)
        {
        }
    }
}