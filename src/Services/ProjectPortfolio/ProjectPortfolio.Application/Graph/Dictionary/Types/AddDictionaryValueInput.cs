namespace ProjectPortfolio.Application.Graph.Dictionary.Types
{
    public record AddDictionaryValueInput(string Name, string DictionaryKey, bool IsActive, string Code, int Sequence);
}