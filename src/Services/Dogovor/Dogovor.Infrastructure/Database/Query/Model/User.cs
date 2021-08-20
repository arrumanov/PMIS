using Dogovor.CrossCutting.Interfaces;

namespace Dogovor.Infrastructure.Database.Query.Model
{
    public class User : IQueryModel
    {
        public string Id { get; set; }
        public string SmallName { get; set; }
    }
}