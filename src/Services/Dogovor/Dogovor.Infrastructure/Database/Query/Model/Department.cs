using System;
using Dogovor.CrossCutting.Interfaces;

namespace Dogovor.Infrastructure.Database.Query.Model
{
    public class Department : IQueryModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}