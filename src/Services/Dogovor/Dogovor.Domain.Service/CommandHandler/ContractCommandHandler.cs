using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dogovor.Application.Commands.Contract;
using Dogovor.CrossCutting.Extensions;
using Dogovor.Domain.Model;
using Dogovor.Infrastructure.Database.Command.Interfaces;
using Dogovor.Infrastructure.ServiceBus;
using MediatR;
using Command = Dogovor.Infrastructure.Database.Command.Model;
using Query = Dogovor.Infrastructure.Database.Query.Model.Contract;

namespace Dogovor.Domain.Service.CommandHandler
{
    public class ContractCommandHandler : IRequestHandler<AddContractCommand, Infrastructure.Database.Query.Model.Contract.Contract>,
        IRequestHandler<UpdateContractInfoCommand, Infrastructure.Database.Query.Model.Contract.Contract>
    {

        private readonly IUnitOfWork _UnitOfWork;
        private readonly IServiceBus _Bus;
        private readonly IContractRepository _ContractRepository;
        private readonly IMapper _Mapper;

        public ContractCommandHandler(IUnitOfWork unitOfWork,
            IServiceBus bus,
            IContractRepository contractRepository,
            IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Bus = bus;
            _ContractRepository = contractRepository;
            _Mapper = mapper;
        }

        public async Task<Infrastructure.Database.Query.Model.Contract.Contract> Handle(AddContractCommand request, CancellationToken cancellationToken)
        {
            var contractDomain = new Contract(request.Name);
            contractDomain.Validate();

            #region Persistence

            var contract = contractDomain.ToModel<Command.Contract>(_Mapper);

            await _ContractRepository.Add(contract);
            await _UnitOfWork.Commit();

            #endregion

            #region Bus

            var publishMessage = new Message();
            publishMessage.MessageType = "AddContract";
            var response = contractDomain.ToQueryModel<Query.Contract>(_Mapper);
            publishMessage.SetData(response);

            await _Bus.SendMessage(publishMessage);

            #endregion

            return response;
        }

        public async Task<Infrastructure.Database.Query.Model.Contract.Contract> Handle(UpdateContractInfoCommand request, CancellationToken cancellationToken)
        {
            var contractDomain = _ContractRepository.GetById(request.Id).Result.ToDomain<Contract>(_Mapper);
            contractDomain.Validate();

            contractDomain.SetInfo(request.Name);

            #region Persistence

            await _ContractRepository.Update(contractDomain.ToModel<Command.Contract>(_Mapper));
            await _UnitOfWork.Commit();

            #endregion

            #region Bus

            var publishMessage = new Message();
            publishMessage.MessageType = "UpdateContract";
            var response = contractDomain.ToQueryModel<Query.Contract>(_Mapper);
            publishMessage.SetData(response);

            await _Bus.SendMessage(publishMessage);

            #endregion

            return response;
        }
    }
}