using Dogovor.Infrastructure.Database.Command.Interfaces;
using Dogovor.Infrastructure.Database.Command.Model;

namespace Dogovor.Infrastructure.Database.Command.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(GraphContext context) : base(context)
        {
        }
    }
}