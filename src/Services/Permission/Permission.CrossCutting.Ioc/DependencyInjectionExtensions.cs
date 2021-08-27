using Permission.Application.Graph.User.Query;
using Permission.Application.MessageHandler;
using Permission.Domain.Service.Mappings;
using Permission.Infrastructure.Database;
using Permission.Infrastructure.Database.Command;
using Permission.Infrastructure.Database.Command.Interfaces;
using Permission.Infrastructure.Database.Command.Repository;
using Permission.Infrastructure.Database.Query;
using Permission.Infrastructure.Database.Query.Manager;
using Permission.Infrastructure.Database.Query.Model;
using Permission.Infrastructure.ServiceBus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Permission.Application.Query.User;
using Permission.Domain.Service.QueryHandler;

namespace Permission.CrossCutting.Ioc
{
    public static class DependencyInjectionExtensions
    {
        #region Infrastructure
        public static void ResolveCommandDatabase(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<PermissionContext>((serviceProvider, opts) =>
            {
                opts.SetConnectionConfig(serviceProvider.GetService<IOptions<DatabaseConfiguration>>());
            });
            
            serviceCollection.AddScoped<IUserRepository, UserRepository>();

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ResolveQueryDatabase(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ManagerFactory>();
            
            serviceCollection.AddSingleton((sp) => sp.GetRequiredService<ManagerFactory>().GetManager<User>());
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
            serviceCollection.AddMediatR(typeof(UserQueryHandler));
        }

        #endregion

        public static void ResolveGraphDependencies(this IServiceCollection serviceCollection, bool testing = false)
        {

            #region User

            #region Query

            serviceCollection.AddSingleton<UserQuery>();

            #endregion

            #endregion

        }

        public static void ResolveAuxiliaries(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(UserProfile));
        }
    }
}