namespace Dogovor.Application.Graph.Common
{
    public class UserError
    {
        public UserError(string message, string code)
        {
            Message = message;
            Code = code;
        }

        public string Message { get; }

        public string Code { get; }
    }
}