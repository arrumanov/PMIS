using System;
using System.Linq;
using ProjectPortfolio.CrossCutting.Exceptions;
using ProjectPortfolio.CrossCutting.Interfaces;
using ProjectPortfolio.Domain.Validator;

namespace ProjectPortfolio.Domain.Model
{
    public class Dictionary : IDomain
    {
        public Dictionary(string dictionaryKey, string name)
        {
            Id = Guid.NewGuid();
            DictionaryKey = dictionaryKey;
            Name = name;
        }

        public Dictionary(Guid id, string dictionaryKey, string name)
        {
            Id = id;
            DictionaryKey = dictionaryKey;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string DictionaryKey { get; private set; }
        public string Name { get; private set; }

        public void Validate()
        {
            var validator = new DictionaryValidator();

            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid) throw new ValidationException(string.Join(";", validationResult.Errors.Select(i => i.ErrorCode)));
        }
    }
}