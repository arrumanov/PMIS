using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ProjectPortfolio.CrossCutting.Extensions;
using ProjectPortfolio.Infrastructure.Database.Query.Manager;
using ProjectPortfolio.Infrastructure.Database.Query.Model.Project;
using ProjectPortfolio.Infrastructure.Database.Query.Model.User;
using ProjectPortfolio.Infrastructure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using TaskSchema = ProjectPortfolio.Infrastructure.Database.Query.Model.Task;

namespace ProjectPortfolio.Application.MessageHandler
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
            user.Projects = new List<UserProject>();

            var manager = scope.ServiceProvider.GetRequiredService<IEntityManager<User>>();

            await manager.Index(user);
        }

        private async Task UpdateUser(Message message, IServiceScope scope)
        {
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(message.MessageData);

            var manager = scope.ServiceProvider.GetRequiredService<IEntityManager<User>>();

            await manager.Remove(Guid.Parse(user.Id));
            await manager.Index(user);
        }

        #endregion

        #region Project

        private async Task AddProject(Message message, IServiceScope scope)
        {
            var project = Newtonsoft.Json.JsonConvert.DeserializeObject<Project>(message.MessageData);

            var manager = scope.ServiceProvider.GetRequiredService<IEntityManager<Project>>();

            await manager.Index(project);
        }
        private async Task UpdateUserProject(Message message, IServiceScope scope)
        {
            var messageData = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectUserMessage>(message.MessageData);

            var projectManager = scope.ServiceProvider.GetRequiredService<IEntityManager<Project>>();
            var userManager = scope.ServiceProvider.GetRequiredService<IEntityManager<User>>();

            await projectManager.Remove(Guid.Parse(messageData.Project.Id));
            await projectManager.Index(messageData.Project);

            await userManager.Remove(Guid.Parse(messageData.User.Id));
            await userManager.Index(messageData.User);
        }

        private async Task AddUpdateTaskProject(Message message, IServiceScope scope)
        {
            var messageData = Newtonsoft.Json.JsonConvert.DeserializeObject<TaskProjectMessage>(message.MessageData);

            var projectManager = scope.ServiceProvider.GetRequiredService<IEntityManager<Project>>();
            var taskManager = scope.ServiceProvider.GetRequiredService<IEntityManager<TaskSchema.Task>>();

            await projectManager.Remove(Guid.Parse(messageData.Project.Id));
            await projectManager.Index(messageData.Project);

            await taskManager.Remove(Guid.Parse(messageData.Task.Id));
            await taskManager.Index(messageData.Task);
        }

        #endregion

        #region Task

        private async Task UpdateTask(Message message, IServiceScope scope)
        {
            var messageData = Newtonsoft.Json.JsonConvert.DeserializeObject<TaskSchema.Task>(message.MessageData);

            var taskManager = scope.ServiceProvider.GetRequiredService<IEntityManager<TaskSchema.Task>>();

            await taskManager.Remove(Guid.Parse(messageData.Id));
            await taskManager.Index(messageData);
        }

        #endregion

    }
}