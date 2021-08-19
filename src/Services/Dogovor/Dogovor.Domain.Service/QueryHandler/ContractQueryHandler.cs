using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dogovor.Application.Query.Contract;
using Dogovor.CrossCutting.Extensions;
using Dogovor.CrossCutting.Extensions.GraphQL;
using Dogovor.Infrastructure.Database.Command.Interfaces;
using Dogovor.Infrastructure.ServiceBus;
using MediatR;

namespace Dogovor.Domain.Service.QueryHandler
{
    using ContractQuery = Infrastructure.Database.Query.Model.Contract.Contract;

    public class ContractQueryHandler : IRequestHandler<GetContractCommand, IQueryable<ContractQuery>>
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IServiceBus _Bus;
        private readonly IContractRepository _ContractRepository;
        private readonly IMapper _Mapper;

        public ContractQueryHandler(IUnitOfWork unitOfWork,
            IServiceBus bus,
            IContractRepository contractRepository,
            IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Bus = bus;
            _ContractRepository = contractRepository;
            _Mapper = mapper;
        }

        public async Task<IQueryable<ContractQuery>> Handle(GetContractCommand request, CancellationToken cancellationToken)
        {
            #region Persistence
            
            var contractsDomain = await _ContractRepository.Get(request.GraphFilters);
            var response = contractsDomain.Select(item => 
                item.ToQueryModel<ContractQuery>(_Mapper));

            #endregion

            return response;
        }
    }
}