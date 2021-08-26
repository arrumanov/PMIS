using System;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.Infrastructure.Database.Command.Model
{
    public class ProjectProduct : IModel
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public Guid ProductId { get; set; }
    }
}