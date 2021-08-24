using System;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.Infrastructure.Database.Command.Model.Dictionary
{
    public class Dictionary : IModel
    {
        public Guid Id { get; set; }
        
        public string DictionaryKey { get; set; }
        
        public string Name { get; set; }
    }
}