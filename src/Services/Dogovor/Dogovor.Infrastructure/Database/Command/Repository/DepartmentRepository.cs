using Dogovor.Infrastructure.Database.Command.Interfaces;
using Dogovor.Infrastructure.Database.Command.Model;

namespace Dogovor.Infrastructure.Database.Command.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(GraphContext context) : base(context)
        {
        }
    }
}