using GraphQL.Types;

namespace Dogovor.Application.Graph.Common
{
    public class UpdateDescriptionInput : InputObjectGraphType<object>
    {
        public UpdateDescriptionInput()
        {
            Field<IdGraphType>().Name("id");
            Field<StringGraphType>().Name("description");
            Field<StringGraphType>().Name("longDescription");
        }
    }
}