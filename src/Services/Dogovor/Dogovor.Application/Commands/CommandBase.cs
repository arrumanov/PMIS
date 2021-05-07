using MediatR;

namespace Dogovor.Application.Commands
{
    public abstract class CommandBase<T> : IRequest<T>
    {
    }
}