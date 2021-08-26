using System;
using ProjectPortfolio.Domain.Model;
using FluentValidation;

namespace ProjectPortfolio.Domain.Validator
{
    public class ProjectDepartmentValidator : AbstractValidator<ProjectDepartment>
    {
        public ProjectDepartmentValidator()
        {
            RuleFor(i => i.Id).NotNull().NotEqual(Guid.Empty).WithErrorCode("ID-01");
            RuleFor(i => i.ProjectId).NotNull().NotEqual(Guid.Empty).WithErrorCode("ID-01");
            RuleFor(i => i.DepartmentId).NotNull().NotEqual(Guid.Empty).WithErrorCode("ID-01");
        }
    }
}