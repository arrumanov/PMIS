using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Permission.CrossCutting.Extensions;
using Permission.Infrastructure.Database.Query.Manager;
using Permission.Infrastructure.Database.Query.Model;
using Permission.Infrastructure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;

namespace Permission.Application.MessageHandler
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

        #region User

        private async Task AddUser(Message message, IServiceScope scope)
        {
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(message.MessageData);

            var manager = scope.ServiceProvider.GetRequiredService<IEntityManager<User>>();

            await manager.Index(user);
        }

        private async Task UpdateUser(Message message, IServiceScope scope)
        {
            var messageData = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(message.MessageData);

            var userManagerManager = scope.ServiceProvider.GetRequiredService<IEntityManager<User>>();

            await userManagerManager.Remove(Guid.Parse(messageData.Id));
            await userManagerManager.Index(messageData);
        }

        #endregion
    }
}