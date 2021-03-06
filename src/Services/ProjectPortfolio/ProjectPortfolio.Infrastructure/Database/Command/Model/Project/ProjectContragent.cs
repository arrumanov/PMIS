using System;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.Infrastructure.Database.Command.Model
{
    public class ProjectContragent : IModel
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public Guid ContragentId { get; set; }
    }
}