using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Permission.Application.Query.User;
using Permission.CrossCutting.Extensions;
using Permission.Infrastructure.Database.Command.Interfaces;
using Permission.Infrastructure.ServiceBus;
using MediatR;

namespace Permission.Domain.Service.QueryHandler
{
    using UserQuery = Infrastructure.Database.Query.Model.User;

    public class UserQueryHandler : IRequestHandler<GetUserCommand, IQueryable<UserQuery>>,
        IRequestHandler<GetUserByIdQuery, UserQuery>
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

        public async Task<UserQuery> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            #region Persistence

            var UserDomain = await _UserRepository.GetById(request.Id);
            var response = UserDomain.ToQueryModel<UserQuery>(_Mapper);

            #endregion

            return response;
        }
    }
}