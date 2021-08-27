using System;
using FluentValidation;
using ProjectPortfolio.Domain.Model;

namespace ProjectPortfolio.Domain.Validator
{
    public class ProjectMetricValidator : AbstractValidator<ProjectMetric>
    {
        public ProjectMetricValidator()
        {
            RuleFor(i => i.Id).NotNull().NotEmpty().NotEqual(Guid.Empty).WithErrorCode("ID-01");
            RuleFor(i => i.Name).NotNull().NotEmpty().WithErrorCode("NAME-01");
            RuleFor(i => i.Description).NotNull().NotEmpty().WithErrorCode("NAME-01");
        }
    }
}