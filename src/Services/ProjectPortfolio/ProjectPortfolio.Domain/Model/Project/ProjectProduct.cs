using System;
using System.Linq;
using ProjectPortfolio.CrossCutting.Exceptions;
using ProjectPortfolio.CrossCutting.Interfaces;
using ProjectPortfolio.Domain.Validator;

namespace ProjectPortfolio.Domain.Model
{
    public class ProjectProduct : IDomain
    {
        public ProjectProduct(Guid projectId, Guid productId)
        {
            Id = Guid.NewGuid();
            ProjectId = projectId;
            ProductId = productId;
        }

        public Guid Id { get; private set; }

        public Guid ProductId { get; private set; }

        public Guid ProjectId { get; private set; }

        public void Validate()
        {
            var validator = new ProjectProductValidator();

            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid) throw new ValidationException(string.Join(";", validationResult.Errors.Select(i => i.ErrorCode)));
        }
    }
}