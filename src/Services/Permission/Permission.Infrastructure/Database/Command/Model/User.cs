using System;
using Permission.CrossCutting.Interfaces;

namespace Permission.Infrastructure.Database.Command.Model
{
    public class User : IModel
    {
        public Guid Id { get; set; }
        public string SmallName { get; set; }
    }
}