using System;
using System.Linq;
using Dogovor.CrossCutting.Exceptions;
using Dogovor.CrossCutting.Interfaces;
using Dogovor.Domain.Validator;

namespace Dogovor.Domain.Model
{
    public class Contragent : IDomain
    {
        public Contragent(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
        public Contragent(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public void SetInfo(string name)
        {
            this.Name = name;

            this.Validate();
        }

        public void Validate()
        {
            var validator = new ContragentValidator();

            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid) throw new ValidationException(string.Join(";", validationResult.Errors.Select(i => i.ErrorCode)));
        }
    }
}