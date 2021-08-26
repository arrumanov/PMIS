using System;
using ProjectPortfolio.Domain.Model;
using FluentValidation;

namespace ProjectPortfolio.Domain.Validator
{
    public class ProjectContragentValidator : AbstractValidator<ProjectContragent>
    {
        public ProjectContragentValidator()
        {
            RuleFor(i => i.Id).NotNull().NotEqual(Guid.Empty).WithErrorCode("ID-01");
            RuleFor(i => i.ProjectId).NotNull().NotEqual(Guid.Empty).WithErrorCode("ID-01");
            RuleFor(i => i.ContragentId).NotNull().NotEqual(Guid.Empty).WithErrorCode("ID-01");
        }
    }
}