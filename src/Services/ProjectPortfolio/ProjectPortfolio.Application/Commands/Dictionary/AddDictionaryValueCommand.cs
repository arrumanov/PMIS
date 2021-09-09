namespace ProjectPortfolio.Application.Commands.Dictionary
{
    public class AddDictionaryValueCommand : CommandBase<Infrastructure.Database.Query.Model.Dictionary.DictionaryValue>
    {
        public string Name { get; set; }
        public string DictionaryKey { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public int Sequence { get; set; }
    }
}