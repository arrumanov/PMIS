using Dogovor.CrossCutting.Interfaces;

namespace Dogovor.Infrastructure.Database.Query.Model.Client
{
    public class Client : IQueryModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}