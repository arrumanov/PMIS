using System.Collections.Generic;

namespace Dogovor.CrossCutting.Extensions.GraphQL
{
    public class GraphFilters
    {
        public IDictionary<string, GraphFilter> Filters { get; set; }
    }
}