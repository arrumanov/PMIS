using Microsoft.EntityFrameworkCore;

namespace PMIS.DogovorGql.Data
{
    public class DogovorDbContext : DbContext
    {
        public DogovorDbContext(DbContextOptions<DogovorDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contract> Contracts { get; set; } = default!;
    }
}