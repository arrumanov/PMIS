using GraphQL.Types;
using Model = Dogovor.Infrastructure.Database.Query.Model.User;

namespace Dogovor.Application.Graph.User.Types.Query
{
    public class UserProjectType : ObjectGraphType<Model.UserProject>
    {
        public UserProjectType()
        {
            Field(i => i.Id);
            Field(i => i.Description);
            Field(i => i.LongDescription);
        }
    }
}
