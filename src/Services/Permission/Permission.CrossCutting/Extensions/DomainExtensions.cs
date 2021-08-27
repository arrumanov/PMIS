using AutoMapper;
using Permission.CrossCutting.Exceptions;
using Permission.CrossCutting.Interfaces;

namespace Permission.CrossCutting.Extensions
{
    public static class DomainExtensions
    {
        public static D ToDomain<D>(this IModel fromRepository, IMapper mapper) where D : IDomain
        {
            if (fromRepository == null) throw new ElementNotFoundException();

            var domain = mapper.Map<D>(fromRepository);

            return domain;
        }

        public static M ToQueryModel<M>(this IModel fromRepository, IMapper mapper) where M : IQueryModel
        {
            if (fromRepository == null) throw new ElementNotFoundException();

            var domain = mapper.Map<M>(fromRepository);

            return domain;
        }

        public static T ToModel<T>(this IDomain domain, IMapper mapper) where T : IModel
        {
            var commandModel = mapper.Map<T>(domain);

            return commandModel;
        }

        public static R ToQueryModel<R>(this IDomain domain, IMapper mapper) where R : IQueryModel
        {
            var queryModel = mapper.Map<R>(domain);

            return queryModel;
        }
    }
}