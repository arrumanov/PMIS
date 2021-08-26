using System;
using System.Linq;
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
            RuleFor(x => x.ProjectDepartments).Must(collection => collection != null && collection.Count != 0).WithErrorCode("PROJECTDEPARTMENTS-01");
            RuleFor(x => x.ProjectContragents).Must(collection => collection != null && collection.Count != 0).WithErrorCode("PROJECTCONTRAGENTS-01");
            RuleFor(x => x.ProjectProducts).Must(collection => collection != null && collection.Count != 0).WithErrorCode("PROJECTPRODUCTS-01");
        }
    }
}