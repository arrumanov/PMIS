using MediatR;

namespace ProjectPortfolio.Application.Commands
{
    public abstract class CommandBase<T> : IRequest<T>
    {
    }
}