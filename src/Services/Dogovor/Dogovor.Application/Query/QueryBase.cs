using MediatR;

namespace Dogovor.Application.Query
{
    public abstract class QueryBase<T> : IRequest<T>
    {
        
    }
}