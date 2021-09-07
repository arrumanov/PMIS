using System;
using Camunda.Worker;
using Camunda.Worker.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Workflow.Api.Bpmn
{
    public static class BpmnInstaller
    {
        public static IServiceCollection AddCamunda(this IServiceCollection services, string camundaRestApiUri)
        {
            services.AddSingleton(_ => new BpmnService(camundaRestApiUri));
            services.AddHostedService<BpmnProcessDeployService>();

            services.AddCamundaWorker(options =>
                {
                    options.BaseUri = new Uri(camundaRestApiUri);
                    options.WorkerCount = 1;
                })
                .AddHandler<NotifyUserTaskHandler>()
                .AddHandler<ProjectCreatedInJiraTaskHandler>()
                .AddHandler<ProjectNotCreatedInJiraTaskHandler>();
                //.AddHandler<CreateProjectInJiraTaskHandler>();

            return services;
        }
    }
}