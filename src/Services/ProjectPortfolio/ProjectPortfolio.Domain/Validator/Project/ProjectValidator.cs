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
            RuleFor(i => i.ResponsibleDepartmentId).NotNull().NotEqual(Guid.Empty).WithErrorCode("RESPONSIBLEDEPARTMENTID-01");
            RuleFor(i => i.InitiatorId).NotNull().NotEqual(Guid.Empty).WithErrorCode("INITIATORID-01");
            RuleFor(i => i.CuratorId).NotNull().NotEqual(Guid.Empty).WithErrorCode("CURATORID-01");
            RuleFor(i => i.ManagerId).NotNull().NotEqual(Guid.Empty).WithErrorCode("MANAGERID-01");
            RuleFor(i => i.CreatorId).NotNull().NotEqual(Guid.Empty).WithErrorCode("CREATORID-01");
            RuleFor(x => x.ProjectDepartments).Must(collection => collection != null && collection.Count != 0).WithErrorCode("PROJECTDEPARTMENTS-01");
            RuleFor(x => x.ProjectContragents).Must(collection => collection != null && collection.Count != 0).WithErrorCode("PROJECTCONTRAGENTS-01");
            RuleFor(x => x.ProjectProducts).Must(collection => collection != null && collection.Count != 0).WithErrorCode("PROJECTPRODUCTS-01");
        }
    }
}