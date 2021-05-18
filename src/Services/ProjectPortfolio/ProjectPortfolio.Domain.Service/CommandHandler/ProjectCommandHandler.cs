using System.Linq;
using AutoMapper;
using Command = ProjectPortfolio.Infrastructure.Database.Command.Model;
using Query = ProjectPortfolio.Infrastructure.Database.Query.Model.Project;
using UserQuery = ProjectPortfolio.Infrastructure.Database.Query.Model.User;
using ProjectPortfolio.Infrastructure.Database.Command.Interfaces;
using MediatR;
using ProjectPortfolio.CrossCutting.Extensions;
using System.Threading;
using System.Threading.Tasks;
using ProjectPortfolio.Infrastructure.ServiceBus;
using ProjectPortfolio.Application.Commands.Project;
using ProjectPortfolio.Domain.Model;

namespace ProjectPortfolio.Domain.Service.CommandHandler
{
    public class ProjectCommandHandler : IRequestHandler<AddProjectCommand, bool>,
                                         IRequestHandler<UpdateProjectInfoCommand, bool>,
                                         IRequestHandler<AddUserProjectCommand, bool>,
                                         IRequestHandler<RemoveUserProjectCommand, bool>
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IServiceBus _Bus;
        private readonly IProjectRepository _ProjectRepository;
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _Mapper;

        public ProjectCommandHandler(IUnitOfWork unitOfWork,
                                     IServiceBus bus,
                                     IProjectRepository projectRepository,
                                     IUserRepository userRepository,
                                     IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Bus = bus;
            _ProjectRepository = projectRepository;
            _UserRepository = userRepository;
            _Mapper = mapper;
        }

        public async Task<bool> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            var projectDomain = new Project(request.Description, request.LongDescription);
            projectDomain.Validate();

            #region Persistence

            var project = projectDomain.ToModel<Command.Project>(_Mapper);

            await _ProjectRepository.Add(project);
            await _UnitOfWork.Commit();

            #endregion

            #region Bus

            var publishMessage = new Message();
            publishMessage.MessageType = "AddProject";
            publishMessage.SetData(projectDomain.ToQueryModel<Query.Project>(_Mapper));

            await _Bus.SendMessage(publishMessage);

            #endregion

            return true;
        }

        public async Task<bool> Handle(AddUserProjectCommand request, CancellationToken cancellationToken)
        {
            var userDomain = _UserRepository.GetById(request.UserId).Result.ToDomain<User>(_Mapper);
            var projectDomain = _ProjectRepository.GetById(request.ProjectId).Result.ToDomain<Project>(_Mapper);

            projectDomain.Validate();
            projectDomain.AddUser(userDomain);

            #region Persistence

            await _ProjectRepository.AddUser(projectDomain.ToModel<Command.UserProject>(_Mapper));
            await _UnitOfWork.Commit();

            #endregion

            #region Bus

            var addUserProjectMessage = new Message();
            addUserProjectMessage.MessageType = "UpdateUserProject";
            addUserProjectMessage.SetData(new
            {
                Project = projectDomain.ToQueryModel<Query.Project>(_Mapper),
                User = userDomain.ToQueryModel<UserQuery.User>(_Mapper)
            }); 

            await _Bus.SendMessage(addUserProjectMessage);

            #endregion

            return true;
        }

        public async Task<bool> Handle(RemoveUserProjectCommand request, CancellationToken cancellationToken)
        {
            var userDomain = _UserRepository.GetById(request.UserId).Result.ToDomain<User>(_Mapper);
            var projectDomain = _ProjectRepository.GetById(request.ProjectId).Result.ToDomain<Project>(_Mapper);

            projectDomain.Validate();
            projectDomain.RemoveUser(userDomain);

            #region Persistence

            await _ProjectRepository.RemoveUser(projectDomain.ToModel<Command.UserProject>(_Mapper));
            await _UnitOfWork.Commit();

            #endregion

            #region Bus

            var addUserProjectMessage = new Message();
            addUserProjectMessage.MessageType = "UpdateUserProject";
            addUserProjectMessage.SetData(new
            {
                Project = projectDomain.ToQueryModel<Query.Project>(_Mapper),
                User = userDomain.ToQueryModel<UserQuery.User>(_Mapper)
            });

            await _Bus.SendMessage(addUserProjectMessage);

            #endregion

            return true;
        }

        

        public async Task<bool> Handle(UpdateProjectInfoCommand request, CancellationToken cancellationToken)
        {
            var tt = _ProjectRepository.GetById(request.Id).Result;
            var tt2 = _ProjectRepository.GetAll().Result.ToList();
            var projectDomain = tt.ToDomain<Project>(_Mapper);
            projectDomain.Validate();

            projectDomain.SetDescription(request.Description, request.LongDescription);

            #region Persistence

            await _ProjectRepository.Update(projectDomain.ToModel<Command.Project>(_Mapper));
            await _UnitOfWork.Commit();

            #endregion

            #region Bus

            var publishMessage = new Message();
            publishMessage.MessageType = "UpdateProject";
            publishMessage.SetData(projectDomain.ToQueryModel<Query.Project>(_Mapper));

            await _Bus.SendMessage(publishMessage);

            #endregion

            return true;
        }
    }
}
