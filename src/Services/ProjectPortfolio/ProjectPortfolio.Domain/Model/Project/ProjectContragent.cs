using System;
using System.Linq;
using ProjectPortfolio.CrossCutting.Exceptions;
using ProjectPortfolio.CrossCutting.Interfaces;
using ProjectPortfolio.Domain.Validator;

namespace ProjectPortfolio.Domain.Model
{
    public class ProjectContragent : IDomain
    {
        public ProjectContragent(Guid projectId, Guid contragentId)
        {
            Id = Guid.NewGuid();
            ProjectId = projectId;
            ContragentId = contragentId;
        }

        public Guid Id { get; private set; }
        
        public Guid ContragentId { get; private set; }

        public Guid ProjectId { get; private set; }


        public void Validate()
        {
            var validator = new ProjectContragentValidator();

            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid) throw new ValidationException(string.Join(";", validationResult.Errors.Select(i => i.ErrorCode)));
        }
    }
}