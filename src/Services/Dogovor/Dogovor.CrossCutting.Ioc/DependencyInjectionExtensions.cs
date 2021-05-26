using Dogovor.Application.Commands.Contract;
using Dogovor.Application.Graph.Contract.Mutation;
using Dogovor.Application.Graph.Contract.Query;
using Dogovor.Application.MessageHandler;
using Dogovor.Domain.Service.CommandHandler;
using Dogovor.Domain.Service.Mappings;
using Dogovor.Infrastructure.Database;
using Dogovor.Infrastructure.Database.Command;
using Dogovor.Infrastructure.Database.Command.Interfaces;
using Dogovor.Infrastructure.Database.Command.Repository;
using Dogovor.Infrastructure.Database.Query;
using Dogovor.Infrastructure.Database.Query.Manager;
using Dogovor.Infrastructure.Database.Query.Model.Contract;
using Dogovor.Infrastructure.ServiceBus;
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
            
            serviceCollection.AddScoped<IContractRepository, ContractRepository>();

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ResolveQueryDatabase(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ManagerFactory>();
            
            serviceCollection.AddSingleton((sp) => sp.GetRequiredService<ManagerFactory>().GetManager<Contract>());
            
            serviceCollection.AddSingleton<IEntityManager<Contract>, ContractManager>();
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
            serviceCollection.AddScoped<IRequestHandler<AddContractCommand, Contract>, ContractCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<UpdateContractInfoCommand, Contract>, ContractCommandHandler>();
        }

        #endregion

        public static void ResolveGraphDependencies(this IServiceCollection serviceCollection, bool testing = false)
        {

            #region Contract

            #region Query

            serviceCollection.AddSingleton<ContractQuery>();

            #endregion

            #region Mutation

            serviceCollection.AddSingleton<ContractMutation>();

            #endregion

            #endregion
            
        }

        public static void ResolveAuxiliaries(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(ContractProfile));
        }
    }
}