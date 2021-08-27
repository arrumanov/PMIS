using System;
using System.Linq;
using ProjectPortfolio.CrossCutting.Exceptions;
using ProjectPortfolio.CrossCutting.Interfaces;
using ProjectPortfolio.Domain.Validator;

namespace ProjectPortfolio.Domain.Model
{
    public class ProjectDepartment : IDomain
    {
        public ProjectDepartment(Guid projectId, Guid departmentId)
        {
            Id = Guid.NewGuid();
            ProjectId = projectId;
            DepartmentId = departmentId;
        }

        public Guid Id { get; private set; }

        public Guid DepartmentId { get; private set; }

        public Guid ProjectId { get; private set; }

        public void Validate()
        {
            var validator = new ProjectDepartmentValidator();

            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid) throw new ValidationException(string.Join(";", validationResult.Errors.Select(i => i.ErrorCode)));
        }
    }
}