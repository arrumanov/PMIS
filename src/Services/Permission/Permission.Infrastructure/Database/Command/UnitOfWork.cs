using System.Threading;
using Permission.Infrastructure.Database.Command.Interfaces;
using Microsoft.EntityFrameworkCore;
using Thread = System.Threading.Tasks;

namespace Permission.Infrastructure.Database.Command
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PermissionContext _Context;

        public UnitOfWork(PermissionContext context)
        {
            _Context = context;
        }
        public async Thread.Task Commit()
        {
            if (_Context.Database.IsInMemory()) return;

            using (var transaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    await _Context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}