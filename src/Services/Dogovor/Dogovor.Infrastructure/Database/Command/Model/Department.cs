using System;
using Dogovor.CrossCutting.Interfaces;

namespace Dogovor.Infrastructure.Database.Command.Model
{
    public class Department : IModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}