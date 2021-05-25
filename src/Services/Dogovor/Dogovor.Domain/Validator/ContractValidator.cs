using System;
using FluentValidation;
using Dogovor.Domain.Model;

namespace Dogovor.Domain.Validator
{
    public class ContractValidator : AbstractValidator<Contract>
    {
        public ContractValidator()
        {
            RuleFor(i => i.Id).NotNull().NotEqual(Guid.Empty).WithErrorCode("ID-01");
            RuleFor(i => i.Name).NotNull().NotEmpty().WithErrorCode("NAME-01");
            RuleFor(i => i.CreatedDate).NotNull().NotEmpty().WithErrorCode("CREATE-01");
        }
    }
}