namespace ProjectPortfolio.CrossCutting.Interfaces
{
    #region Позже нужно будет удалить

    public class GraphFilter
    {
        public GraphFilter()
        {

        }
        public GraphFilter(string operation, object value)
        {
            Operation = operation;
            Value = value;
        }

        public string Operation { get; set; }
        public object Value { get; set; }
    }

    #endregion

    public interface IQueryModel
    {
        string Id { get; set; }
    }
}