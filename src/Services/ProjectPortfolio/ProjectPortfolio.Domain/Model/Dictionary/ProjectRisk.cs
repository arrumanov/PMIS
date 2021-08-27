using System;
using System.Linq;
using ProjectPortfolio.CrossCutting.Exceptions;
using ProjectPortfolio.CrossCutting.Interfaces;
using ProjectPortfolio.Domain.Validator;

namespace ProjectPortfolio.Domain.Model
{
    public class ProjectRisk : IDomain
    {
        public ProjectRisk(Guid productId, string name, string conditionOffensive, 
            string probabilityOffensive, string degreeEffect, string methodOfResponse)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Name = name;
            ConditionOffensive = conditionOffensive;
            ProbabilityOffensive = probabilityOffensive;
            DegreeEffect = degreeEffect;
            MethodOfResponse = methodOfResponse;
        }

        public ProjectRisk(Guid id, Guid productId, string name, string conditionOffensive,
            string probabilityOffensive, string degreeEffect, string methodOfResponse)
        {
            Id = id;
            ProductId = productId;
            Name = name;
            ConditionOffensive = conditionOffensive;
            ProbabilityOffensive = probabilityOffensive;
            DegreeEffect = degreeEffect;
            MethodOfResponse = methodOfResponse;
        }

        public Guid Id { get; private set; }
        
        public Guid ProductId { get; private set; }
        
        public string Name { get; private set; }
        
        public string ConditionOffensive { get; private set; }
        
        public string ProbabilityOffensive { get; private set; }
        
        public string DegreeEffect { get; private set; }
        
        public string MethodOfResponse { get; private set; }

        public void Validate()
        {
            var validator = new ProjectRiskValidator();

            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid) throw new ValidationException(string.Join(";", validationResult.Errors.Select(i => i.ErrorCode)));
        }
    }
}