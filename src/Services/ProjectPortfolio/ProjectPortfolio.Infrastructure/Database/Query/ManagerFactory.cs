using ProjectPortfolio.CrossCutting.Interfaces;
using Microsoft.Extensions.Options;

//https://logz.io/blog/elasticsearch-vs-mongodb/
//https://mindmajix.com/mongodb-vs-elasticsearch
namespace ProjectPortfolio.Infrastructure.Database.Query
{
    public class ManagerFactory
    {
        private readonly IOptions<DatabaseConfiguration> _Configuration;
        public ManagerFactory(IOptions<DatabaseConfiguration> options)
        {
            _Configuration = options;
        }

        public IManager<T> GetManager<T>() where T : class, IQueryModel
        {
            switch (_Configuration.Value.ReadDatabaseProvider)
            {
                case DatabaseProvider.MONGODB:
                    return new MongoManager<T>(_Configuration);

                case DatabaseProvider.ELASTICSEARCH:
                    return new ElasticSearchManager<T>(_Configuration);

                default:
                    return new InMemoryManager<T>();
            }
        }
    }
}