using System;
using FluentValidation;
using ProjectPortfolio.Domain.Model;

namespace ProjectPortfolio.Domain.Validator
{
    public class ProjectRiskValidator : AbstractValidator<ProjectRisk>
    {
        public ProjectRiskValidator()
        {
            RuleFor(i => i.Id).NotNull().NotEmpty().NotEqual(Guid.Empty).WithErrorCode("ID-01");
            RuleFor(i => i.ProductId).NotNull().NotEmpty().NotEqual(Guid.Empty).WithErrorCode("PRODUCTID-01");
            RuleFor(i => i.Name).NotNull().NotEmpty().WithErrorCode("NAME-01");
            RuleFor(i => i.ConditionOffensive).NotNull().NotEmpty().WithErrorCode("CONDITIONOFFENSIVE-01");
            RuleFor(i => i.ProbabilityOffensive).NotNull().NotEmpty().WithErrorCode("PROBABILITYOFFENSIVE-01");
            RuleFor(i => i.DegreeEffect).NotNull().NotEmpty().WithErrorCode("DEGREEEFFECT-01");
            RuleFor(i => i.MethodOfResponse).NotNull().NotEmpty().WithErrorCode("METHODOFRESPONSE-01");
        }
    }
}