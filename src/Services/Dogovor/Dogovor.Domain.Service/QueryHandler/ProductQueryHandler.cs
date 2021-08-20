using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dogovor.Application.Query.Product;
using Dogovor.CrossCutting.Extensions;
using Dogovor.Infrastructure.Database.Command.Interfaces;
using Dogovor.Infrastructure.ServiceBus;
using MediatR;

namespace Dogovor.Domain.Service.QueryHandler
{
    using ProductQuery = Infrastructure.Database.Query.Model.Product;

    public class ProductQueryHandler : IRequestHandler<GetProductCommand, IQueryable<ProductQuery>>
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IServiceBus _Bus;
        private readonly IProductRepository _ProductRepository;
        private readonly IMapper _Mapper;

        public ProductQueryHandler(IUnitOfWork unitOfWork,
            IServiceBus bus,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Bus = bus;
            _ProductRepository = productRepository;
            _Mapper = mapper;
        }

        public async Task<IQueryable<ProductQuery>> Handle(GetProductCommand request, CancellationToken cancellationToken)
        {
            #region Persistence
            
            var contragentsDomain = await _ProductRepository.Get(request.GraphFilters);
            var response = contragentsDomain.Select(item => 
                item.ToQueryModel<ProductQuery>(_Mapper));

            #endregion

            return response;
        }
    }
}