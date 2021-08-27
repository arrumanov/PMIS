using System;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.Infrastructure.Database.Command.Model
{
    public class ProjectMetric : IModel
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public int Sequence { get; set; }
    }
}