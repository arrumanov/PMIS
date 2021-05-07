using Dogovor.Application.Commands.Project;
using Dogovor.Application.Commands.Task;
using Dogovor.Application.Commands.User;
using Dogovor.Application.Graph;
using Dogovor.Application.Graph.Common;
using Dogovor.Application.Graph.Project.Mutation;
using Dogovor.Application.Graph.Project.Query;
using Dogovor.Application.Graph.Project.Types.Input;
using Dogovor.Application.Graph.Project.Types.Query;
using Dogovor.Application.Graph.Task.Mutation;
using Dogovor.Application.Graph.Task.Query;
using Dogovor.Application.Graph.Task.Types.Input;
using Dogovor.Application.Graph.Task.Types.Query;
using Dogovor.Application.Graph.User.Mutation;
using Dogovor.Application.Graph.User.Query;
using Dogovor.Application.Graph.User.Types.Input;
using Dogovor.Application.Graph.User.Types.Query;
using Dogovor.Application.MessageHandler;
using Dogovor.Domain.Service.CommandHandler;
using Dogovor.Domain.Service.Mappings;
using Dogovor.Infrastructure.Database;
using Dogovor.Infrastructure.Database.Command;
using Dogovor.Infrastructure.Database.Command.Interfaces;
using Dogovor.Infrastructure.Database.Command.Repository;
using Dogovor.Infrastructure.Database.Query;
using Dogovor.Infrastructure.Database.Query.Manager;
using Dogovor.Infrastructure.Database.Query.Model.Project;
using Dogovor.Infrastructure.Database.Query.Model.Task;
using Dogovor.Infrastructure.Database.Query.Model.User;
using Dogovor.Infrastructure.ServiceBus;
using GraphQL;
using GraphQL.Types;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Dogovor.CrossCutting.Ioc
{
    public static class DependencyInjectionExtensions
    {
        #region Infrastructure
        public static void ResolveCommandDatabase(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<GraphContext>((serviceProvider, opts) => {
                opts.SetConnectionConfig(serviceProvider.GetService<IOptions<DatabaseConfiguration>>());
            });

            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IProjectRepository, ProjectRepository>();
            serviceCollection.AddScoped<ITaskRepository, TaskRepository>();

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ResolveQueryDatabase(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ManagerFactory>();

            serviceCollection.AddSingleton((sp) => sp.GetRequiredService<ManagerFactory>().GetManager<User>());
            serviceCollection.AddSingleton((sp) => sp.GetRequiredService<ManagerFactory>().GetManager<Project>());
            serviceCollection.AddSingleton((sp) => sp.GetRequiredService<ManagerFactory>().GetManager<Task>());

            serviceCollection.AddSingleton<IEntityManager<User>, UserManager>();
            serviceCollection.AddSingleton<IEntityManager<Project>, ProjectManager>();
            serviceCollection.AddSingleton<IEntityManager<Task>, TaskManager>();
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
            serviceCollection.AddMediatR(typeof(UserCommandHandler), typeof(ProjectCommandHandler), typeof(TaskCommandHandler));

            serviceCollection.AddScoped<IRequestHandler<AddUserCommand, bool>, UserCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<UpdateUserInfoCommand, bool>, UserCommandHandler>();

            serviceCollection.AddScoped<IRequestHandler<AddProjectCommand, bool>, ProjectCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<AddUserProjectCommand, bool>, ProjectCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<RemoveUserProjectCommand, bool>, ProjectCommandHandler>();

            serviceCollection.AddScoped<IRequestHandler<AddTaskCommand, bool>, TaskCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<RemoveTaskCommand, bool>, TaskCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<ChangeAssigneeCommand, bool>, TaskCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<UpdateTaskStatusCommand, bool>, TaskCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<UpdateDeadlineCommand, bool>, TaskCommandHandler>();
        }

        #endregion

        public static void ResolveGraphDependencies(this IServiceCollection serviceCollection, bool testing = false)
        {
            serviceCollection.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            serviceCollection.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            #region Common


            serviceCollection.AddSingleton<MutationResultType>();

            serviceCollection.AddSingleton<UpdateDescriptionInput>();

            serviceCollection.AddSingleton<TaskStatusEnumType>();

            serviceCollection.AddSingleton<PaginationType>();
            serviceCollection.AddSingleton<FilterType>();
            serviceCollection.AddSingleton<EnumFilterType>();
            serviceCollection.AddSingleton<DateFilterType>();

            #endregion

            #region User

            #region Query

            serviceCollection.AddSingleton<UserQuery>();

            serviceCollection.AddSingleton<UserFilterType>();

            serviceCollection.AddSingleton<UserType>();
            serviceCollection.AddSingleton<UserProjectType>();

            #endregion

            #region Mutation

            serviceCollection.AddSingleton<UserMutation>();

            serviceCollection.AddSingleton<AddUserInput>();
            serviceCollection.AddSingleton<EditUserInput>();

            #endregion

            #endregion

            #region Project

            #region Query

            serviceCollection.AddSingleton<ProjectQuery>();

            serviceCollection.AddSingleton<ProjectFilterType>();

            serviceCollection.AddSingleton<ProjectType>();
            serviceCollection.AddSingleton<ProjectUserType>();
            serviceCollection.AddSingleton<ProjectTaskType>();

            #endregion

            #region Mutation

            serviceCollection.AddSingleton<ProjectMutation>();

            serviceCollection.AddSingleton<UserProjectInput>();
            serviceCollection.AddSingleton<AddProjectInput>();

            #endregion

            #endregion

            #region Task

            #region Query

            serviceCollection.AddSingleton<TaskQuery>();

            serviceCollection.AddSingleton<TaskFilterType>();

            serviceCollection.AddSingleton<TaskType>();
            serviceCollection.AddSingleton<TaskUserType>();
            serviceCollection.AddSingleton<TaskProjectType>();

            #endregion

            #region Mutation

            serviceCollection.AddSingleton<TaskMutation>();

            serviceCollection.AddSingleton<AddTaskInput>();
            serviceCollection.AddSingleton<ChangeAssigneeInput>();
            serviceCollection.AddSingleton<UpdateDeadlineInput>();
            serviceCollection.AddSingleton<UpdateTaskStatusInput>();

            #endregion

            #endregion

            if (!testing) serviceCollection.AddSingleton<ISchema, GraphSchema>();
        }

        public static void ResolveAuxiliaries(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(UserProfile),
                                            typeof(ProjectProfile),
                                            typeof(TaskProfile),
                                            typeof(StatusProfile));
        }
    }
}