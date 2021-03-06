using System;

namespace Dogovor.Application.Commands.Contract
{
    public class UpdateContractInfoCommand : CommandBase<Infrastructure.Database.Query.Model.Contract>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}