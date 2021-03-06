using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dogovor.Application.Query.Contragent;
using Dogovor.CrossCutting.Extensions;
using Dogovor.Infrastructure.Database.Command.Interfaces;
using Dogovor.Infrastructure.ServiceBus;
using MediatR;

namespace Dogovor.Domain.Service.QueryHandler
{
    using ContragentQuery = Infrastructure.Database.Query.Model.Contragent;

    public class ContragentQueryHandler : IRequestHandler<GetContragentCommand, IQueryable<ContragentQuery>>,
        IRequestHandler<GetContragentByIdQuery, ContragentQuery>,
        IRequestHandler<GetContragentsByIdsQuery, IEnumerable<ContragentQuery>>
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IServiceBus _Bus;
        private readonly IContragentRepository _ContragentRepository;
        private readonly IMapper _Mapper;

        public ContragentQueryHandler(IUnitOfWork unitOfWork,
            IServiceBus bus,
            IContragentRepository contragentRepository,
            IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Bus = bus;
            _ContragentRepository = contragentRepository;
            _Mapper = mapper;
        }

        public async Task<IQueryable<ContragentQuery>> Handle(GetContragentCommand request, CancellationToken cancellationToken)
        {
            #region Persistence
            
            var contragentsDomain = await _ContragentRepository.Get(request.GraphFilters);
            var response = contragentsDomain.Select(item => 
                item.ToQueryModel<ContragentQuery>(_Mapper));

            #endregion

            return response;
        }

        public async Task<ContragentQuery> Handle(GetContragentByIdQuery request, CancellationToken cancellationToken)
        {
            #region Persistence

            var contragentDomain = await _ContragentRepository.GetById(request.Id);
            var response = contragentDomain.ToQueryModel<ContragentQuery>(_Mapper);

            #endregion

            return response;
        }

        public async Task<IEnumerable<ContragentQuery>> Handle(GetContragentsByIdsQuery request, CancellationToken cancellationToken)
        {
            #region Persistence

            var contragentsDomain = await _ContragentRepository.GetByIds(request.Ids);
            var response = contragentsDomain.Select(item => item.ToQueryModel<ContragentQuery>(_Mapper));

            #endregion

            return response;
        }
    }
}