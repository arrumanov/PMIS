using MediatR;

namespace Permission.Application.Commands
{
    public abstract class CommandBase<T> : IRequest<T>
    {
    }
}