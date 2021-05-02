using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nest;
using PMIS.Contracts;

namespace PMIS.ProjectGql.IntegrationEvents
{
    public class ProjectIndexService : IProjectIndexService
    {
        private readonly IServiceProvider _serviceProvider;
        protected readonly ElasticSearchSettings _configurationSettings;
        private readonly ILogger<ProjectIndexService> _logger;

        public ProjectIndexService(IServiceProvider serviceProvider, ILogger<ProjectIndexService> logger, ElasticSearchSettings configurationSettings)
        {
            _serviceProvider = serviceProvider;
            _configurationSettings = configurationSettings;
            _logger = logger;
        }

        public async Task IndexProject(List<Project> projects)
        {
            if (projects != null)
            {
                var _ElasticClient = ConnectionHelper.GetClientSingleton(_configurationSettings);

                List<Project> items = projects;

                var descriptor = new BulkDescriptor();

                if (items != null && items.Count > 0)
                    for (int index = 0; index < items.Count; index++)
                    {
                        var indexingItem = items[index];

                        if (indexingItem != null)
                            descriptor.Index<Project>(o => o.Document(indexingItem)
                                .Routing(indexingItem.Id)
                                .Id(indexingItem.Id)
                                .Index(_configurationSettings.AliasName)).Refresh(Elasticsearch.Net.Refresh.True);
                    }

                var response = await _ElasticClient.BulkAsync(descriptor);

                if (!response.IsValid || response.ItemsWithErrors.Any())
                {
                    _logger.LogError(string.Format("Error on IndexItem. isValid : {0} , errorItems : {1} {2}", response.IsValid, response.ItemsWithErrors.Count(), response.ServerError.Error));

                }
            }
        }
    }
}