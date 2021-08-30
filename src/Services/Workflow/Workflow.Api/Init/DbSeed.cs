using System.Threading.Tasks;
using Workflow.Api.DataAccess;

namespace Workflow.Api.Init
{
    public class DbSeed
    {
        private readonly WorkflowContext db;

        public DbSeed(WorkflowContext db)
        {
            this.db = db;
        }

        public async Task SeedData()
        {

        }
    }
}