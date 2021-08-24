using System;
using ProjectPortfolio.Domain.Model;
using FluentValidation;

namespace ProjectPortfolio.Domain.Validator
{
    public class ProjectValidator : AbstractValidator<Project>
    {
        public ProjectValidator()
        {
            RuleFor(i => i.Id).NotNull().NotEqual(Guid.Empty).WithErrorCode("ID-01");
            RuleFor(i => i.Name).NotNull().NotEmpty().WithErrorCode("DESC-01");
        }
    }
}