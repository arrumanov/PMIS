using System;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.Infrastructure.Database.Command.Model
{
    public class CustomField : IModel
    {
        public Guid Id { get; set; }
    }
}