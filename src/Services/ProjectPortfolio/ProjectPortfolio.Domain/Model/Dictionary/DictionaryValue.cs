using System;
using System.Linq;
using ProjectPortfolio.CrossCutting.Exceptions;
using ProjectPortfolio.CrossCutting.Interfaces;
using ProjectPortfolio.Domain.Validator;

namespace ProjectPortfolio.Domain.Model
{
    public class DictionaryValue : IDomain
    {
        public DictionaryValue(string dictionaryKey, string name, bool isActive, string code, int sequence)
        {
            Id = Guid.NewGuid();
            DictionaryKey = dictionaryKey;
            Name = name;
            IsActive = isActive;
            Code = code;
            Sequence = sequence;
        }

        public DictionaryValue(Guid id, string dictionaryKey, string name, bool isActive, string code, int sequence)
        {
            Id = id;
            DictionaryKey = dictionaryKey;
            Name = name;
            IsActive = isActive;
            Code = code;
            Sequence = sequence;
        }

        public Guid Id { get; private set; }

        public string DictionaryKey { get; private set; }

        public string Name { get; private set; }

        public bool IsActive { get; private set; }

        public string Code { get; private set; }

        public int Sequence { get; private set; }

        public void Validate()
        {
            var validator = new DictionaryValueValidator();

            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid) throw new ValidationException(string.Join(";", validationResult.Errors.Select(i => i.ErrorCode)));
        }
    }
}