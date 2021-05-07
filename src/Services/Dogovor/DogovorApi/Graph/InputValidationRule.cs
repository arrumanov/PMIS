using GraphQL.Validation;

namespace DogovorApi.Graph
{
    public class InputValidationRule : IValidationRule
    {
        public INodeVisitor Validate(ValidationContext context)
        {
            return new EnterLeaveListener(_ =>
            {
            });
        }
    }
}
