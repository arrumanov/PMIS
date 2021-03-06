using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dogovor.Application.Query.Department;
using Dogovor.CrossCutting.Extensions;
using Dogovor.Infrastructure.Database.Command.Interfaces;
using Dogovor.Infrastructure.ServiceBus;
using MediatR;

namespace Dogovor.Domain.Service.QueryHandler
{
    using DepartmentQuery = Infrastructure.Database.Query.Model.Department;

    public class DepartmentQueryHandler : IRequestHandler<GetDepartmentCommand, IQueryable<DepartmentQuery>>,
        IRequestHandler<GetDepartmentByIdQuery, DepartmentQuery>,
        IRequestHandler<GetDepartmentsByIdsQuery, IEnumerable<DepartmentQuery>>
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IServiceBus _Bus;
        private readonly IDepartmentRepository _DepartmentRepository;
        private readonly IMapper _Mapper;

        public DepartmentQueryHandler(IUnitOfWork unitOfWork,
            IServiceBus bus,
            IDepartmentRepository departmentRepository,
            IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Bus = bus;
            _DepartmentRepository = departmentRepository;
            _Mapper = mapper;
        }

        public async Task<IQueryable<DepartmentQuery>> Handle(GetDepartmentCommand request, CancellationToken cancellationToken)
        {
            #region Persistence
            
            var contragentsDomain = await _DepartmentRepository.Get(request.GraphFilters);
            var response = contragentsDomain.Select(item => 
                item.ToQueryModel<DepartmentQuery>(_Mapper));

            #endregion

            return response;
        }

        public async Task<DepartmentQuery> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            #region Persistence
            
            var departmentDomain = await _DepartmentRepository.GetById(request.Id);
            var response = departmentDomain.ToQueryModel<DepartmentQuery>(_Mapper);

            #endregion

            return response;
        }

        public async Task<IEnumerable<DepartmentQuery>> Handle(GetDepartmentsByIdsQuery request, CancellationToken cancellationToken)
        {
            #region Persistence
            
            var departmentsDomain = await _DepartmentRepository.GetByIds(request.Ids);
            var response = departmentsDomain.Select(item => item.ToQueryModel<DepartmentQuery>(_Mapper));

            #endregion

            return response;
        }
    }
}