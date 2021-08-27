using System;
using System.Linq;
using ProjectPortfolio.CrossCutting.Exceptions;
using ProjectPortfolio.CrossCutting.Interfaces;
using ProjectPortfolio.Domain.Validator;

namespace ProjectPortfolio.Domain.Model
{
    public class ProjectMetric : IDomain
    {
        public ProjectMetric(string name, string description, int sequence)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Sequence = sequence;
        }

        public ProjectMetric(Guid id, string name, string description, int sequence)
        {
            Id = id;
            Name = name;
            Description = description;
            Sequence = sequence;
        }

        public Guid Id { get; private set; }
        
        public string Name { get; private set; }
        
        public string Description { get; private set; }
        
        public int Sequence { get; private set; }

        public void Validate()
        {
            var validator = new ProjectMetricValidator();

            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid) throw new ValidationException(string.Join(";", validationResult.Errors.Select(i => i.ErrorCode)));
        }
    }
}