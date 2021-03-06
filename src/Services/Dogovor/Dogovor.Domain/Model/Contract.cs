using System;
using System.Linq;
using Dogovor.CrossCutting.Exceptions;
using Dogovor.CrossCutting.Interfaces;
using Dogovor.Domain.Validator;

namespace Dogovor.Domain.Model
{
    public class Contract : IDomain
    {
        public Contract(string number)
        {
            Id = Guid.NewGuid();
            Number = number;
        }
        public Contract(Guid id, string number)
        {
            Id = id;
            Number = number;
        }

        public Guid Id { get; private set; }
        public string Number { get; private set; }

        public void SetInfo(string number)
        {
            this.Number = number;

            this.Validate();
        }

        public void Validate()
        {
            var validator = new ContractValidator();

            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid) throw new ValidationException(string.Join(";", validationResult.Errors.Select(i => i.ErrorCode)));
        }
    }
}