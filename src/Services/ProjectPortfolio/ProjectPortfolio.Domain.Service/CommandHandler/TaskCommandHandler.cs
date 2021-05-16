using AutoMapper;
using ProjectPortfolio.Application.Commands.Task;
using ProjectPortfolio.CrossCutting.Extensions;
using ProjectPortfolio.Infrastructure.Database.Command.Interfaces;
using ProjectPortfolio.Infrastructure.ServiceBus;
using MediatR;
using Command = ProjectPortfolio.Infrastructure.Database.Command.Model;
using System.Threading;
using System.Threading.Tasks;
using Query = ProjectPortfolio.Infrastructure.Database.Query.Model.Task;
using ProjectQuery = ProjectPortfolio.Infrastructure.Database.Query.Model.Project;

namespace ProjectPortfolio.Domain.Service.CommandHandler
{
    public class TaskCommandHandler : IRequestHandler<ChangeAssigneeCommand, bool>,
                                      IRequestHandler<UpdateTaskStatusCommand, bool>,
                                      IRequestHandler<UpdateDeadlineCommand, bool>,
                                      IRequestHandler<UpdateTaskInfoCommand, bool>,
                                      IRequestHandler<AddTaskCommand, bool>,
                                      IRequestHandler<RemoveTaskCommand, bool>
    {

        private readonly IUnitOfWork _UnitOfWork;
        private readonly IServiceBus _Bus;
        private readonly IUserRepository _UserRepository;
        private readonly IProjectRepository _ProjectRepository;
        private readonly ITaskRepository _TaskRepository;
        private readonly IMapper _Mapper;

        public TaskCommandHandler(IUnitOfWork unitOfWork,
                                     IServiceBus bus,
                                     ITaskRepository taskRepository,
                                     IUserRepository userRepository,
                                     IProjectRepository projectRepository,
                                     IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Bus = bus;
            _TaskRepository = taskRepository;
            _UserRepository = userRepository;
            _ProjectRepository = projectRepository;
            _Mapper = mapper;
        }

        public async Task<bool> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var task = _TaskRepository.GetById(request.Id).Result.ToDomain<Model.Task>(_Mapper);

            task.SetStatus(request.Status);

            var taskModel = task.ToModel<Command.Task>(_Mapper);

            await _TaskRepository.Update(taskModel);
            await _UnitOfWork.Commit();

            var busMessage = new Message();
            busMessage.MessageType = "AddUpdateTaskProject";
            busMessage.SetData(new
            {
                Project = task.Project.ToQueryModel<ProjectQuery.Project>(_Mapper),
                Task = task.ToQueryModel<Query.Task>(_Mapper)
            });

            await _Bus.SendMessage(busMessage);

            return true;
        }

        public async Task<bool> Handle(ChangeAssigneeCommand request, CancellationToken cancellationToken)
        {
            var task = _TaskRepository.GetById(request.Id).Result.ToDomain<ProjectPortfolio.Domain.Model.Task>(_Mapper);
            var newAssignee = _UserRepository.GetById(request.NewAssigneeId).Result.ToDomain<Model.User>(_Mapper);

            task.ChangeAssignee(newAssignee);

            var taskModel = task.ToModel<Command.Task>(_Mapper);

            await _TaskRepository.Update(taskModel);
            await _UnitOfWork.Commit();

            var busMessage = new Message();
            busMessage.MessageType = "AddUpdateTaskProject";
            busMessage.SetData(new
            {
                Project = task.Project.ToQueryModel<ProjectQuery.Project>(_Mapper),
                Task = task.ToQueryModel<Query.Task>(_Mapper)
            });

            await _Bus.SendMessage(busMessage);

            return true;
        }

        public async Task<bool> Handle(UpdateDeadlineCommand request, CancellationToken cancellationToken)
        {
            var task = _TaskRepository.GetById(request.Id).Result.ToDomain<Model.Task>(_Mapper);

            task.SetDeadline(request.Deadline);

            var taskModel = task.ToModel<Command.Task>(_Mapper);

            await _TaskRepository.Update(taskModel);
            await _UnitOfWork.Commit();

            var busMessage = new Message();
            busMessage.MessageType = "AddUpdateTaskProject";
            busMessage.SetData(new
            {
                Project = task.Project.ToQueryModel<ProjectQuery.Project>(_Mapper),
                Task = task.ToQueryModel<Query.Task>(_Mapper)
            });

            await _Bus.SendMessage(busMessage);

            return true;
        }

        public async Task<bool> Handle(UpdateTaskInfoCommand request, CancellationToken cancellationToken)
        {
            var task = _TaskRepository.GetById(request.Id).Result.ToDomain<Model.Task>(_Mapper);

            task.SetDescription(request.Description, request.LongDescription);

            var taskModel = task.ToModel<Command.Task>(_Mapper);

            await _TaskRepository.Update(taskModel);
            await _UnitOfWork.Commit();

            var busMessage = new Message();
            busMessage.MessageType = "AddUpdateTaskProject";
            busMessage.SetData(new
            {
                Project = task.Project.ToQueryModel<ProjectQuery.Project>(_Mapper),
                Task = task.ToQueryModel<Query.Task>(_Mapper)
            });

            await _Bus.SendMessage(busMessage);

            return true;
        }

        public async Task<bool> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            var projectDomain = _ProjectRepository.GetById(request.ProjectId).Result.ToDomain<ProjectPortfolio.Domain.Model.Project>(_Mapper);
            projectDomain.Validate();

            var assignee = _UserRepository.GetById(request.AssigneeId).Result.ToDomain<Model.User>(_Mapper);
            var reporter = _UserRepository.GetById(request.ReporterId).Result.ToDomain<Model.User>(_Mapper);

            var taskDomain = new Model.Task(request.Description, request.LongDescription, request.DeadLine, assignee, reporter, projectDomain);
            projectDomain.AddTask(taskDomain);

            #region Persistence

            await _TaskRepository.Add(taskDomain.ToModel<Command.Task>(_Mapper));
            await _UnitOfWork.Commit();

            #endregion

            #region Bus

            var addTaskToProject = new Message();
            addTaskToProject.MessageType = "AddUpdateTaskProject";
            addTaskToProject.SetData(new
            {
                Project = projectDomain.ToQueryModel<ProjectQuery.Project>(_Mapper),
                Task = taskDomain.ToQueryModel<Query.Task>(_Mapper)
            });

            await _Bus.SendMessage(addTaskToProject);

            #endregion

            return true;
        }

        public async Task<bool> Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
        {
            var taskDomain = _TaskRepository.GetById(request.Id).Result.ToDomain<Model.Task>(_Mapper);
            var projectDomain = taskDomain.Project;
            projectDomain.Validate();

            projectDomain.RemoveTask(taskDomain);

            #region Persistence

            await _TaskRepository.Remove(taskDomain.ToModel<Command.Task>(_Mapper));
            await _UnitOfWork.Commit();

            #endregion

            #region Bus

            var removeTaskProject = new Message();
            removeTaskProject.MessageType = "AddUpdateTaskProject";
            removeTaskProject.SetData(new
            {
                Project = projectDomain.ToQueryModel<ProjectQuery.Project>(_Mapper),
                Task = taskDomain.ToQueryModel<Query.Task>(_Mapper)
            });

            await _Bus.SendMessage(removeTaskProject);

            #endregion

            return true;
        }
    }
}
