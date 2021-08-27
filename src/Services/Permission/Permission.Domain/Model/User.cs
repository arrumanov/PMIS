using System;
using System.Linq;
using Permission.CrossCutting.Exceptions;
using Permission.CrossCutting.Interfaces;
using Permission.Domain.Validator;

namespace Permission.Domain.Model
{
    public class User : IDomain
    {
        public User(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
        public User(Guid id, string name)
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
            var validator = new UserValidator();

            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid) throw new ValidationException(string.Join(";", validationResult.Errors.Select(i => i.ErrorCode)));
        }
    }
}