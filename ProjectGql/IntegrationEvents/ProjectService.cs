using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PMIS.Contracts;

namespace PMIS.ProjectGql.IntegrationEvents
{
    public class ProjectService : IProjectService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProjectService> _logger;

        public ProjectService(IServiceProvider serviceProvider, ILogger<ProjectService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task Save(List<Project> projects)
        {
            var projectEntities = projects.Select(x => new Project()
            {
                Name = x.Name,
                Price = x.Price,
                Quantity = x.Quantity,
                CreationDate = x.CreationDate,
                LastModifyDate = x.LastModifyDate
            }).ToList();

            var repository = _serviceProvider.GetService<IProjectRepository>();
            await repository.AddRange(projectEntities);
        }
    }
}