﻿using AutoMapper;
using ProjectPortfolio.Domain.Model;
using Command = ProjectPortfolio.Infrastructure.Database.Command.Model;
using Query = ProjectPortfolio.Infrastructure.Database.Query.Model.Dictionary;

namespace ProjectPortfolio.Domain.Service.Mappings
{
    public class DictionaryProfile : Profile
    {
        public DictionaryProfile()
        {
            CreateMap<DictionaryValue, Command.DictionaryValue>();

            CreateMap<Command.DictionaryValue, DictionaryValue>();

            CreateMap<DictionaryValue, Query.DictionaryValue>();
        }
    }
}