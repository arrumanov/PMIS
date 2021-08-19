using System;
using System.Collections.Generic;

namespace Dogovor.CrossCutting.Extensions.GraphQL
{
    public class GraphFilter
    {
        public GraphFilter()
        {

        }
        public GraphFilter(string operation, string value)
        {
            Operation = operation;
            StringValue = value;
        }

        public string Operation { get; set; }
        public string StringValue { get; set; }
        public List<string> StringValues { get; set; }
    }
}