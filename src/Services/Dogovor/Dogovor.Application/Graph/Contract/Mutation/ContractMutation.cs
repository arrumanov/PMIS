using System;
using System.Collections.Generic;
using System.Threading;
using AutoMapper.Internal;
using Dogovor.Application.Commands.Contract;
using Dogovor.Application.Graph.Common;
using Dogovor.Application.Graph.Contract.Types;
using Dogovor.CrossCutting.Exceptions;
using HotChocolate;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using HotChocolate.Types;

namespace Dogovor.Application.Graph.Contract.Mutation
{
    [ExtendObjectType(Name = "Mutation")]
    public class ContractMutation
    {
        public async Task<AddContractPayload> AddContract(
            AddContractInput input,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var addContractCommand = new AddContractCommand()
            {
                Name = input.Name
            };
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                try
                {
                    var response = await mediator.Send(addContractCommand, cancellationToken);
                    return new AddContractPayload(response);
                }
                catch (ValidationException e)
                {
                    var userErrors = new List<UserError>();
                    e.Message.Split(";").ForAll(item =>
                    {
                        userErrors.Add(new UserError(item, item));
                    });
                    return new AddContractPayload(userErrors);
                }
            }
        }

        public async Task<AddContractPayload> UpdateContractInfo(
            UpdateContractInfoInput input,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var updateContractInfoCommand = new UpdateContractInfoCommand()
            {
                Id = input.Id,
                Name = input.Name
            };
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                try
                {
                    var response = await mediator.Send(updateContractInfoCommand, cancellationToken);
                    return new AddContractPayload(response);
                }
                catch (ValidationException e)
                {
                    var userErrors = new List<UserError>();
                    e.Message.Split(";").ForAll(item => { userErrors.Add(new UserError(item, item)); });
                    return new AddContractPayload(userErrors);
                }
                catch (ElementNotFoundException e)
                {
                    return new AddContractPayload(new List<UserError>
                    {
                        new UserError("Элемент не найден", "PROJ-not_found")
                    });
                }
                catch (QueryArgumentException e)
                {
                    return new AddContractPayload(new List<UserError>
                    {
                        new UserError(e.Message, e.Message)
                    });
                }
            }
        }
    }
}