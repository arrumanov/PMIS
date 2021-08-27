using System;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.Infrastructure.Database.Command.Model
{
    public class ProjectRisk : IModel
    {
        public Guid Id { get; set; }
        
        public Guid ProductId { get; set; }
        
        public string Name { get; set; }
        
        public string ConditionOffensive { get; set; }
        
        public string ProbabilityOffensive { get; set; }
        
        public string DegreeEffect { get; set; }
        
        public string MethodOfResponse { get; set; }
    }
}