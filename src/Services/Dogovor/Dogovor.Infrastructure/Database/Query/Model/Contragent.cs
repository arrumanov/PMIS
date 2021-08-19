using Dogovor.CrossCutting.Interfaces;

namespace Dogovor.Infrastructure.Database.Query.Model
{
    public class Contragent : IQueryModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}