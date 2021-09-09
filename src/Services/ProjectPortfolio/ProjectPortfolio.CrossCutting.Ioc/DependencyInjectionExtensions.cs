using ProjectPortfolio.Application.Commands.Project;
using ProjectPortfolio.Application.Commands.Task;
using ProjectPortfolio.Application.Commands.User;
using ProjectPortfolio.Application.MessageHandler;
using ProjectPortfolio.Domain.Service.CommandHandler;
using ProjectPortfolio.Domain.Service.Mappings;
using ProjectPortfolio.Infrastructure.Database;
using ProjectPortfolio.Infrastructure.Database.Command;
using ProjectPortfolio.Infrastructure.Database.Command.Interfaces;
using ProjectPortfolio.Infrastructure.Database.Command.Repository;
using ProjectPortfolio.Infrastructure.Database.Query;
using ProjectPortfolio.Infrastructure.Database.Query.Manager;
using ProjectPortfolio.Infrastructure.Database.Query.Model.Project;
using ProjectPortfolio.Infrastructure.Database.Query.Model.Task;
using ProjectPortfolio.Infrastructure.Database.Query.Model.User;
using ProjectPortfolio.Infrastructure.ServiceBus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProjectPortfolio.Application.Graph.Dictionary.Mutation;
using ProjectPortfolio.Application.Graph.Dictionary.Query;
using ProjectPortfolio.Application.Graph.Project.Mutation;
using ProjectPortfolio.Application.Graph.Project.Query;
using ProjectPortfolio.Infrastructure.Database.Query.Model.Dictionary;

namespace ProjectPortfolio.CrossCutting.Ioc
{
    public static class DependencyInjectionExtensions
    {
        #region Infrastructure
        public static void ResolveCommandDatabase(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<ProjectPortfolioContext>((serviceProvider, opts) => {
                opts.SetConnectionConfig(serviceProvider.GetService<IOptions<DatabaseConfiguration>>());
            });

            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IProjectRepository, ProjectRepository>();
            serviceCollection.AddScoped<ITaskRepository, TaskRepository>();
            serviceCollection.AddScoped<IDictionaryRepository, DictionaryRepository>();
            serviceCollection.AddScoped<IDictionaryValueRepository, DictionaryValueRepository>();

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ResolveQueryDatabase(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ManagerFactory>();

            serviceCollection.AddSingleton((sp) => sp.GetRequiredService<ManagerFactory>().GetManager<User>());
            serviceCollection.AddSingleton((sp) => sp.GetRequiredService<ManagerFactory>().GetManager<Project>());
            serviceCollection.AddSingleton((sp) => sp.GetRequiredService<ManagerFactory>().GetManager<Task>());
            serviceCollection.AddSingleton((sp) => sp.GetRequiredService<ManagerFactory>().GetManager<Dictionary>());
            serviceCollection.AddSingleton((sp) => sp.GetRequiredService<ManagerFactory>().GetManager<DictionaryValue>());

            serviceCollection.AddSingleton<IEntityManager<User>, UserManager>();
            serviceCollection.AddSingleton<IEntityManager<Project>, ProjectManager>();
            serviceCollection.AddSingleton<IEntityManager<Task>, TaskManager>();
            serviceCollection.AddSingleton<IEntityManager<Dictionary>, DictionaryManager>();
            serviceCollection.AddSingleton<IEntityManager<DictionaryValue>, DictionaryValueManager>();
        }

        public static void ResolveServiceBus(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IServiceBus, MassTransitSB>();
            serviceCollection.AddSingleton<ISubscribe, BusMessageHandler>();
        }

        #endregion

        #region Domain Service
        public static void ResolveRequestHandlers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(typeof(UserCommandHandler), typeof(ProjectCommandHandler), 
                typeof(TaskCommandHandler), typeof(DictionaryCommandHandler));

            serviceCollection.AddScoped<IRequestHandler<AddUserCommand, bool>, UserCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<UpdateUserInfoCommand, bool>, UserCommandHandler>();

            serviceCollection.AddScoped<IRequestHandler<AddProjectCommand, Project>, ProjectCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<AddUserProjectCommand, ProjectUserMessage>, ProjectCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<RemoveUserProjectCommand, ProjectUserMessage>, ProjectCommandHandler>();
            //todo если эту строку удалить, все равно код будет понимать, что нужно вызвать. пока не понял, как это происходит
            serviceCollection.AddScoped<IRequestHandler<UpdateProjectInfoCommand, Project>, ProjectCommandHandler>();

            serviceCollection.AddScoped<IRequestHandler<AddTaskCommand, bool>, TaskCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<RemoveTaskCommand, bool>, TaskCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<ChangeAssigneeCommand, bool>, TaskCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<UpdateTaskStatusCommand, bool>, TaskCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<UpdateDeadlineCommand, bool>, TaskCommandHandler>();
        }

        #endregion

        public static void ResolveGraphDependencies(this IServiceCollection serviceCollection, bool testing = false)
        {

            #region Common
            


            #endregion

            #region User

            #region Query
            

            #endregion

            #region Mutation
            

            #endregion

            #endregion

            #region Project

            #region Query
            
            serviceCollection.AddSingleton<ProjectQuery>();

            #endregion

            #region Mutation

            serviceCollection.AddSingleton<ProjectMutation>();

            #endregion

            #endregion

            #region Task

            #region Query


            #endregion

            #region Mutation


            #endregion

            #endregion

            #region Dictionary

            #region Query

            serviceCollection.AddSingleton<DictionaryQuery>();
            serviceCollection.AddSingleton<DictionaryValueQuery>();

            #endregion

            #region Mutation

            serviceCollection.AddSingleton<DictionaryMutation>();
            serviceCollection.AddSingleton<DictionaryValueMutation>();

            #endregion

            #endregion

        }

        public static void ResolveAuxiliaries(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(UserProfile),
                                            typeof(ProjectProfile),
                                            typeof(TaskProfile),
                                            typeof(StatusProfile),
                                            typeof(DictionaryProfile));
        }
    }
}