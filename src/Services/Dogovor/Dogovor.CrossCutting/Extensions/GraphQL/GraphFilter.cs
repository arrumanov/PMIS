namespace Dogovor.CrossCutting.Extensions.GraphQL
{
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
}