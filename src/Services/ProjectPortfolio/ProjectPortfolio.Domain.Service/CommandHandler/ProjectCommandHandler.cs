using System;
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
using AutoMapper.Internal;
using CrossCutting;
using MassTransit;
using ProjectPortfolio.Infrastructure.ServiceBus;
using ProjectPortfolio.Application.Commands.Project;
using ProjectPortfolio.Application.MessageHandler;
using ProjectPortfolio.CrossCutting;
using ProjectPortfolio.Domain.Model;

namespace ProjectPortfolio.Domain.Service.CommandHandler
{
    public class ProjectCommandHandler : IRequestHandler<AddProjectCommand, Infrastructure.Database.Query.Model.Project.Project>,
                                         IRequestHandler<UpdateProjectInfoCommand, Infrastructure.Database.Query.Model.Project.Project>,
                                         IRequestHandler<AddUserProjectCommand, ProjectUserMessage>,
                                         IRequestHandler<RemoveUserProjectCommand, ProjectUserMessage>
    {
        private readonly IUnitOfWork _UnitOfWork;
        //private readonly IServiceBus _Bus;
        private readonly IPublishEndpoint _PublishEndpoint;
        private readonly ISendEndpointProvider _SendEndpointProvider;
        private readonly IProjectRepository _ProjectRepository;
        private readonly IDictionaryValueRepository _DictionaryValueRepository;
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _Mapper;

        public ProjectCommandHandler(IUnitOfWork unitOfWork,
                                     //IServiceBus bus,
                                     IPublishEndpoint publishEndpoint,
                                     ISendEndpointProvider sendEndpointProvider,
                                     IProjectRepository projectRepository,
                                     IDictionaryValueRepository dictionaryValueRepository,
                                     IUserRepository userRepository,
                                     IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            //_Bus = bus;
            _PublishEndpoint = publishEndpoint;
            _SendEndpointProvider = sendEndpointProvider;
            _ProjectRepository = projectRepository;
            _DictionaryValueRepository = dictionaryValueRepository;
            _UserRepository = userRepository;
            _Mapper = mapper;
        }

        public async Task<Infrastructure.Database.Query.Model.Project.Project> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            var projectDomain = new Project(request.Name, request.Description, 
                request.CategoryId, request.TypeId, request.ResponsibleDepartmentId,
                request.InitiatorId, request.CuratorId, request.ManagerId);
            var categoryDomain = _DictionaryValueRepository.GetById(request.CategoryId).Result
                .ToDomain<DictionaryValue>(_Mapper);
            projectDomain.Category = categoryDomain;
            var typeDomain = _DictionaryValueRepository.GetById(request.TypeId).Result
                .ToDomain<DictionaryValue>(_Mapper);
            projectDomain.Type = typeDomain;


            var projectDepartmentsDomain = request.DepartmentIds.Select(item =>
            {
                var newItem = new ProjectDepartment(projectDomain.Id, item);
                newItem.Validate();
                return newItem;
            });
            var projectContragentsDomain = request.ContragentIds.Select(item => {
                var newItem = new ProjectContragent(projectDomain.Id, item);
                newItem.Validate();
                return newItem;
            });
            var projectproductsDomain = request.ProductIds.Select(item => {
                var newItem = new ProjectProduct(projectDomain.Id, item);
                newItem.Validate();
                return newItem;
            });

            projectDepartmentsDomain.ForAll(item => projectDomain.AddDepartment(item));
            projectContragentsDomain.ForAll(item => projectDomain.AddContragent(item));
            projectproductsDomain.ForAll(item => projectDomain.AddProduct(item));

            projectDomain.Validate();

            #region Persistence

            var project = projectDomain.ToModel<Command.Project>(_Mapper);
            await _ProjectRepository.Add(project);
            await _UnitOfWork.Commit();

            #endregion

            #region Bus

            var publishMessage = new Message();
            publishMessage.MessageType = "AddProject";
            var response = projectDomain.ToQueryModel<Query.Project>(_Mapper);
            publishMessage.SetData(response);

            //await _Bus.SendMessage(publishMessage);
            await _SendEndpointProvider.Send(publishMessage, cancellationToken);

            await _PublishEndpoint.Publish<ProjectCreated>(new
            {
                Id = project.Id,
                Name = project.Name
            }, cancellationToken);

            #endregion

            return response;
        }

        public async Task<ProjectUserMessage> Handle(AddUserProjectCommand request, CancellationToken cancellationToken)
        {
            var userDomain = _UserRepository.GetById(request.UserId).Result.ToDomain<User>(_Mapper);
            var projectDomain = _ProjectRepository.GetById(request.ProjectId).Result.ToDomain<Project>(_Mapper);

            projectDomain.Validate();
            //projectDomain.AddUser(userDomain);

            #region Persistence

            await _ProjectRepository.AddUser(projectDomain.ToModel<Command.UserProject>(_Mapper));
            await _UnitOfWork.Commit();

            #endregion

            #region Bus

            var addUserProjectMessage = new Message();
            addUserProjectMessage.MessageType = "UpdateUserProject";
            var response = new ProjectUserMessage
            {
                Project = projectDomain.ToQueryModel<Query.Project>(_Mapper),
                User = userDomain.ToQueryModel<UserQuery.User>(_Mapper)
            };
            addUserProjectMessage.SetData(response);

            await _SendEndpointProvider.Send(addUserProjectMessage, cancellationToken);

            #endregion

            return response;
        }

        public async Task<ProjectUserMessage> Handle(RemoveUserProjectCommand request, CancellationToken cancellationToken)
        {
            var userDomain = _UserRepository.GetById(request.UserId).Result.ToDomain<User>(_Mapper);
            var projectDomain = _ProjectRepository.GetById(request.ProjectId).Result.ToDomain<Project>(_Mapper);

            projectDomain.Validate();
            //projectDomain.RemoveUser(userDomain);

            #region Persistence

            await _ProjectRepository.RemoveUser(projectDomain.ToModel<Command.UserProject>(_Mapper));
            await _UnitOfWork.Commit();

            #endregion

            #region Bus

            var addUserProjectMessage = new Message();
            addUserProjectMessage.MessageType = "UpdateUserProject";
            var response = new ProjectUserMessage
            {
                Project = projectDomain.ToQueryModel<Query.Project>(_Mapper),
                User = userDomain.ToQueryModel<UserQuery.User>(_Mapper)
            };
            addUserProjectMessage.SetData(response);

            await _SendEndpointProvider.Send(addUserProjectMessage, cancellationToken);

            #endregion

            return response;
        }

        

        public async Task<Infrastructure.Database.Query.Model.Project.Project> Handle(UpdateProjectInfoCommand request, CancellationToken cancellationToken)
        {
            var projectDomain = _ProjectRepository.GetById(request.Id).Result.ToDomain<Project>(_Mapper);
            projectDomain.Validate();

            projectDomain.SetDescription(request.Description, request.LongDescription);

            #region Persistence

            await _ProjectRepository.Update(projectDomain.ToModel<Command.Project>(_Mapper));
            await _UnitOfWork.Commit();

            #endregion

            #region Bus

            var publishMessage = new Message();
            publishMessage.MessageType = "UpdateProject";
            var response = projectDomain.ToQueryModel<Query.Project>(_Mapper);
            publishMessage.SetData(response);

            await _SendEndpointProvider.Send(publishMessage, cancellationToken);

            #endregion

            return response;
        }
    }
}
