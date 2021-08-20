using System;
using Dogovor.CrossCutting.Interfaces;

namespace Dogovor.Infrastructure.Database.Command.Model
{
    public class User : IModel
    {
        public Guid Id { get; set; }
        public string SmallName { get; set; }
    }
}