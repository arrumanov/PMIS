using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dogovor.CrossCutting.Extensions;
using Dogovor.Infrastructure.Database.Query.Manager;
using Dogovor.Infrastructure.Database.Query.Model;
using Dogovor.Infrastructure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;

namespace Dogovor.Application.MessageHandler
{
    public class BusMessageHandler : ISubscribe
    {
        private readonly IServiceProvider _ServiceProvider;

        public BusMessageHandler(IServiceProvider serviceProvider)
        {
            _ServiceProvider = serviceProvider;
        }

        public async Task HandleMessage(Message message)
        {
            using (var scope = _ServiceProvider.CreateScope())
            {
                var command = this.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(i => i.Name.ToLower() == message.MessageType.ToLower());

                if (!command.IsNull())
                {
                    await (Task)command.Invoke(this, new object[] { message, scope });
                }
                else
                {
                    throw new MethodAccessException();
                }
            }
        }

        #region Contract

        private async Task AddContract(Message message, IServiceScope scope)
        {
            var contract = Newtonsoft.Json.JsonConvert.DeserializeObject<Contract>(message.MessageData);

            var manager = scope.ServiceProvider.GetRequiredService<IEntityManager<Contract>>();

            await manager.Index(contract);
        }

        private async Task UpdateContract(Message message, IServiceScope scope)
        {
            var messageData = Newtonsoft.Json.JsonConvert.DeserializeObject<Contract>(message.MessageData);

            var contractManagerManager = scope.ServiceProvider.GetRequiredService<IEntityManager<Contract>>();

            await contractManagerManager.Remove(Guid.Parse(messageData.Id));
            await contractManagerManager.Index(messageData);
        }

        #endregion
    }
}