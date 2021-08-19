using System;
using Dogovor.CrossCutting.Interfaces;

namespace Dogovor.Infrastructure.Database.Command.Model
{
    public class Contract : IModel
    {
        public Contract()
        {

        }

        public Guid Id { get; set; }
        public string Number { get; set; }
        //public DateTime CreatedDate { get; set; }
    }
}