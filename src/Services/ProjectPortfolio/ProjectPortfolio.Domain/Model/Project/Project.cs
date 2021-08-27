using System;
using System.Collections.Generic;
using System.Linq;
using ProjectPortfolio.CrossCutting.Exceptions;
using ProjectPortfolio.CrossCutting.Extensions;
using ProjectPortfolio.CrossCutting.Interfaces;
using ProjectPortfolio.Domain.Validator;

namespace ProjectPortfolio.Domain.Model
{
    public class Project : IDomain
    {
        public Project(string name, string description, Guid responsibleDepartmentId)
        {
            Id = Guid.NewGuid();
            Name = name;
            ResponsibleDepartmentId = responsibleDepartmentId;
            Description = description;
            ProjectDepartments = new List<ProjectDepartment>();
            ProjectContragents = new List<ProjectContragent>();
            ProjectProducts = new List<ProjectProduct>();
            Users = new List<User>();
            Tasks = new List<Task>();
            State = DomainState.NEW;
            CreatedDate = DateTime.UtcNow;
        }

        public Project(Guid id, string name, string description, Guid responsibleDepartmentId,
            ICollection<ProjectDepartment> projectDepartments, ICollection<ProjectContragent> projectContragents, 
            ICollection<ProjectProduct> projectProducts, 
            ICollection<User> users, ICollection<Task> tasks)
        {
            Id = id;
            Name = name;
            Description = description;
            ResponsibleDepartmentId = responsibleDepartmentId;
            ProjectDepartments = projectDepartments;
            ProjectContragents = projectContragents;
            ProjectProducts = projectProducts;
            Users = users;
            Tasks = tasks;
            State = DomainState.FROM_DB;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public Guid ResponsibleDepartmentId { get; private set; }

        public ICollection<ProjectDepartment> ProjectDepartments { get; private set; }
        public ICollection<ProjectProduct> ProjectProducts { get; private set; }
        public ICollection<ProjectContragent> ProjectContragents { get; private set; }
        public DictionaryValue Probability { get; private set; }
        public DictionaryValue Statement { get; private set; }


        public ICollection<User> Users { get; private set; }
        public ICollection<Task> Tasks { get; private set; }
        public DomainState State { get; private set; }

        public void AddDepartment(ProjectDepartment projectDepartment)
        {
            projectDepartment.Validate();

            if (ProjectDepartments.Any(i => i.Id == projectDepartment.Id)) throw new ValidationException("PROJDEPARTMENT-01");

            ProjectDepartments.Add(projectDepartment);
        }

        public void RemoveDepartment(ProjectDepartment projectDepartment)
        {
            projectDepartment.Validate();

            if (!ProjectDepartments.Any(i => i.Id == projectDepartment.Id)) throw new ValidationException("PROJDEPARTMENT-02");

            ProjectDepartments.Update(projectDepartment);


        }

        public void AddContragent(ProjectContragent projectContragent)
        {
            projectContragent.Validate();

            if (ProjectContragents.Any(i => i.Id == projectContragent.Id)) throw new ValidationException("PROJCONTRAGENT-01");

            ProjectContragents.Add(projectContragent);
        }

        public void RemoveContragent(ProjectContragent projectContragent)
        {
            projectContragent.Validate();

            if (!ProjectContragents.Any(i => i.Id == projectContragent.Id)) throw new ValidationException("PROJCONTRAGENT-02");

            ProjectContragents.Update(projectContragent);


        }

        public void AddProduct(ProjectProduct projectProduct)
        {
            projectProduct.Validate();

            if (ProjectProducts.Any(i => i.Id == projectProduct.Id)) throw new ValidationException("PROJPRODUCT-01");

            ProjectProducts.Add(projectProduct);
        }

        public void RemoveProduct(ProjectProduct projectProduct)
        {
            projectProduct.Validate();

            if (!ProjectProducts.Any(i => i.Id == projectProduct.Id)) throw new ValidationException("PROJPRODUCT-02");

            ProjectProducts.Update(projectProduct);


        }

        public void AddUser(User user)
        {
            user.Validate();

            if (Users.Any(i => i.Id == user.Id)) throw new ValidationException("PROJUSER-01");

            user.SetStateForRelation(true);

            user.AddProject(this);
            Users.Add(user);
        }

        public void RemoveUser(User user)
        {
            user.Validate();

            if (!Users.Any(i => i.Id == user.Id)) throw new ValidationException("PROJUSER-02");

            user.SetStateForRelation(false);

            user.RemoveProject(this);
            Users.Update(user);


        }

        public void AddTask(Task task)
        {
            task.Validate();

            if (Tasks.Any(i => i.Id == task.Id)) throw new ValidationException("PROJTASK-01");
            if (!Users.Any(i => i.Id == task.Assignee.Id) || !Users.Any(i => i.Id == task.Reporter.Id)) throw new ValidationException("PROJTASK-02");

            Tasks.Add(task);
        }

        public void UpdateTask(Task task)
        {
            task.Validate();

            var selectedTask = Tasks.FirstOrDefault(i => i.Id == task.Id);

            if (selectedTask.IsNull()) throw new ValidationException("PROJTASK-01");
            if (!Users.Any(i => i.Id == task.Assignee.Id) || !Users.Any(i => i.Id == task.Reporter.Id))
                throw new ValidationException("PROJTASK-02");

            Tasks.Update(task);
        }

        public void RemoveTask(Task task)
        {
            task.Validate();

            if (!Tasks.Any(i => i.Id == task.Id)) throw new ValidationException("PROJTASK-01");

            Tasks.Remove(task);
        }

        public void SetDescription(string name, string description)
        {
            this.Name = name;
            this.Description = description;

            this.Validate();
        }

        public void Validate()
        {
            var validator = new ProjectValidator();

            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid) throw new ValidationException(string.Join(";", validationResult.Errors.Select(i => i.ErrorCode)));
        }
    }
}