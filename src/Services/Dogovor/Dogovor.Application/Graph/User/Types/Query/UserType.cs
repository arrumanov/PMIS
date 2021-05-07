using GraphQL.Types;
using Model = Dogovor.Infrastructure.Database.Query.Model.User;

namespace Dogovor.Application.Graph.User.Types.Query
{
    public class UserType : ObjectGraphType<Model.User>
    {
        public UserType()
        {
            Field(i => i.Id).Name("id");
            Field(i => i.Name).Name("name");
            Field(i => i.Email).Name("email");
            Field<ListGraphType<UserProjectType>>().Name("projects").Resolve((ctx) => ctx.Source.Projects);
        }
    }
}
