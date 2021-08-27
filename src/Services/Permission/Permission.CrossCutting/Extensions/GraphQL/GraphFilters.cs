using System.Collections.Generic;

namespace Permission.CrossCutting.Extensions.GraphQL
{
    public class GraphFilters
    {
        public IDictionary<string, GraphFilter> Filters { get; set; }
    }
}