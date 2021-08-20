using System;
using FluentValidation;
using Dogovor.Domain.Model;

namespace Dogovor.Domain.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(i => i.Id).NotNull().NotEqual(Guid.Empty).WithErrorCode("ID-01");
            RuleFor(i => i.Name).NotNull().NotEmpty().WithErrorCode("NAME-01");
        }
    }
}