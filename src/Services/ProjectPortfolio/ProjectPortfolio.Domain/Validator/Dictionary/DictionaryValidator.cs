using System;
using FluentValidation;
using ProjectPortfolio.Domain.Model;

namespace ProjectPortfolio.Domain.Validator
{
    public class DictionaryValidator : AbstractValidator<Dictionary>
    {
        public DictionaryValidator()
        {
            RuleFor(i => i.Id).NotNull().NotEmpty().NotEqual(Guid.Empty).WithErrorCode("ID-01");
            RuleFor(i => i.DictionaryKey).NotNull().NotEmpty().WithErrorCode("DICTIONARYKEY-01");
            RuleFor(i => i.Name).NotNull().NotEmpty().WithErrorCode("NAME-01");
        }
    }
}