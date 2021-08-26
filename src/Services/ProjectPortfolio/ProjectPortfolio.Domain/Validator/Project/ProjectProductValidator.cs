using System;
using ProjectPortfolio.Domain.Model;
using FluentValidation;

namespace ProjectPortfolio.Domain.Validator
{
    public class ProjectProductValidator : AbstractValidator<ProjectProduct>
    {
        public ProjectProductValidator()
        {
            RuleFor(i => i.Id).NotNull().NotEqual(Guid.Empty).WithErrorCode("ID-01");
            RuleFor(i => i.ProjectId).NotNull().NotEqual(Guid.Empty).WithErrorCode("ID-01");
            RuleFor(i => i.ProductId).NotNull().NotEqual(Guid.Empty).WithErrorCode("ID-01");
        }
    }
}