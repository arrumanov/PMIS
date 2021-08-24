using System;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.Infrastructure.Database.Command.Model
{
    public class DictionaryValue : IModel
    {
        public Guid Id { get; set; }
        
        public string DictionaryKey { get; set; }
        
        public string Name { get; set; }
        
        public bool IsActive { get; set; }
        
        public string Code { get; set; }
        
        public int Sequence { get; set; }
    }
}