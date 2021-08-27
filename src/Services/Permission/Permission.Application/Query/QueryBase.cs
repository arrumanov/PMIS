using MediatR;

namespace Permission.Application.Query
{
    public abstract class QueryBase<T> : IRequest<T>
    {
        
    }
}