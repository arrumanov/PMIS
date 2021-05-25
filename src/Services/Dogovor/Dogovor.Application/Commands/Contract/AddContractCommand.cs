using System;

namespace Dogovor.Application.Commands.Contract
{
    public class AddContractCommand : CommandBase<Infrastructure.Database.Query.Model.Contract.Contract>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}