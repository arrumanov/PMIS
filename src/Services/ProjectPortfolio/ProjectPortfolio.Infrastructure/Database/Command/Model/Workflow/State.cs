using System;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.Infrastructure.Database.Command.Model
{
    public class State : IModel
    {
        public Guid Id { get; set; }
    }
}