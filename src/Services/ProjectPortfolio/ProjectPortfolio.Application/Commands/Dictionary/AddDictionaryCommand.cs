namespace ProjectPortfolio.Application.Commands.Dictionary
{
    public class AddDictionaryCommand : CommandBase<Infrastructure.Database.Query.Model.Dictionary.Dictionary>
    {
        public string Name { get; set; }
        public string DictionaryKey { get; set; }
    }
}