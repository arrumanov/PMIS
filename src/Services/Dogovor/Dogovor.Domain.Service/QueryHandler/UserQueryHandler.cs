using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dogovor.Application.Query.User;
using Dogovor.CrossCutting.Extensions;
using Dogovor.Infrastructure.Database.Command.Interfaces;
using Dogovor.Infrastructure.ServiceBus;
using MediatR;

namespace Dogovor.Domain.Service.QueryHandler
{
    using UserQuery = Infrastructure.Database.Query.Model.User;

    public class UserQueryHandler : IRequestHandler<GetUserCommand, IQueryable<UserQuery>>
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IServiceBus _Bus;
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _Mapper;

        public UserQueryHandler(IUnitOfWork unitOfWork,
            IServiceBus bus,
            IUserRepository UserRepository,
            IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Bus = bus;
            _UserRepository = UserRepository;
            _Mapper = mapper;
        }

        public async Task<IQueryable<UserQuery>> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            #region Persistence
            
            var contragentsDomain = await _UserRepository.Get(request.GraphFilters);
            var response = contragentsDomain.Select(item => 
                item.ToQueryModel<UserQuery>(_Mapper));

            #endregion

            return response;
        }
    }
}