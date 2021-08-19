using System;
using FluentValidation;
using Dogovor.Domain.Model;

namespace Dogovor.Domain.Validator
{
    public class ContragentValidator : AbstractValidator<Contragent>
    {
        public ContragentValidator()
        {
            RuleFor(i => i.Id).NotNull().NotEqual(Guid.Empty).WithErrorCode("ID-01");
            RuleFor(i => i.Name).NotNull().NotEmpty().WithErrorCode("NAME-01");
        }
    }
}