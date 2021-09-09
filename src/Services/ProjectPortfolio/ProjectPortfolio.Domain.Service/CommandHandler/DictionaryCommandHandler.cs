using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ProjectPortfolio.Application.Commands.Dictionary;
using ProjectPortfolio.CrossCutting.Extensions;
using ProjectPortfolio.Infrastructure.Database.Command.Interfaces;
using ProjectPortfolio.Infrastructure.Database.Query.Model.Dictionary;
using ProjectPortfolio.Infrastructure.ServiceBus;
using Command = ProjectPortfolio.Infrastructure.Database.Command.Model;
using Query = ProjectPortfolio.Infrastructure.Database.Query.Model.Dictionary;

namespace ProjectPortfolio.Domain.Service.CommandHandler
{
    public class DictionaryCommandHandler : IRequestHandler<AddDictionaryCommand, Dictionary>,
        IRequestHandler<AddDictionaryValueCommand, DictionaryValue>
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IServiceBus _Bus;
        private readonly IDictionaryRepository _DictionaryRepository;
        private readonly IDictionaryValueRepository _DictionaryValueRepository;
        private readonly IMapper _Mapper;

        public DictionaryCommandHandler(IUnitOfWork unitOfWork,
            IServiceBus bus,
            IDictionaryRepository dictionaryRepository,
            IDictionaryValueRepository dictionaryValueRepository,
            IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Bus = bus;
            _DictionaryRepository = dictionaryRepository;
            _DictionaryValueRepository = dictionaryValueRepository;
            _Mapper = mapper;
        }

        public async Task<Dictionary> Handle(AddDictionaryCommand request, CancellationToken cancellationToken)
        {
            var dictionaryDomain = new Model.Dictionary(request.Name, request.DictionaryKey);
            
            dictionaryDomain.Validate();

            #region Persistence

            var dictionary = dictionaryDomain.ToModel<Command.Dictionary>(_Mapper);

            await _DictionaryRepository.Add(dictionary);
            await _UnitOfWork.Commit();

            #endregion

            #region Bus

            var publishMessage = new Message();
            publishMessage.MessageType = "AddDictionary";
            var response = dictionaryDomain.ToQueryModel<Query.Dictionary>(_Mapper);
            publishMessage.SetData(response);

            await _Bus.SendMessage(publishMessage);

            #endregion

            return response;
        }

        public async Task<DictionaryValue> Handle(AddDictionaryValueCommand request, CancellationToken cancellationToken)
        {
            var dictionaryValueDomain = new Model.DictionaryValue(request.Name, request.DictionaryKey, request.IsActive, request.Code, request.Sequence);

            dictionaryValueDomain.Validate();

            #region Persistence

            var dictionaryValue = dictionaryValueDomain.ToModel<Command.DictionaryValue>(_Mapper);

            await _DictionaryValueRepository.Add(dictionaryValue);
            await _UnitOfWork.Commit();

            #endregion

            #region Bus

            var publishMessage = new Message();
            publishMessage.MessageType = "AddDictionaryValue";
            var response = dictionaryValueDomain.ToQueryModel<Query.DictionaryValue>(_Mapper);
            publishMessage.SetData(response);

            await _Bus.SendMessage(publishMessage);

            #endregion

            return response;
        }
    }
}