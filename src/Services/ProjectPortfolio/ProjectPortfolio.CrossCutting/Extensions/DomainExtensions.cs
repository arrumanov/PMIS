﻿using AutoMapper;
using ProjectPortfolio.CrossCutting.Exceptions;
using ProjectPortfolio.CrossCutting.Interfaces;

namespace ProjectPortfolio.CrossCutting.Extensions
{
    public static class DomainExtensions
    {
        public static D ToDomain<D>(this IModel fromRepository, IMapper mapper) where D : IDomain
        {
            if (fromRepository == null) throw new ElementNotFoundException();

            var domain = mapper.Map<D>(fromRepository);

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