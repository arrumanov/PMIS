using System.Security.Claims;

namespace DogovorApi.Graph
{
    public class GraphQLUserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}
