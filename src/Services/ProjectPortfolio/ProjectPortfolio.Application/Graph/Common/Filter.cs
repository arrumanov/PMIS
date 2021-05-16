namespace ProjectPortfolio.Application.Graph.Common
{
    public class Filter
    {
        public Filter()
        {

        }

        public Filter(string operation, string value)
        {
            Operation = operation;
            Value = value;
        }

        public string Operation { get; set; }
        public string Value { get; set; }
    }
}