using System;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;

namespace DogovorApi.Graph
{
    public class GraphQLSettings
    {
        public PathString Path { get; set; } = "/api/graphql";
        public Func<HttpContext, object> BuildUserContext { get; set; }
        public bool EnableMetrics { get; set; }

        public ISchema Schema { get; set; }
    }
}
