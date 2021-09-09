using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.Internal;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectPortfolio.Application.Commands.Dictionary;
using ProjectPortfolio.Application.Graph.Common;
using ProjectPortfolio.Application.Graph.Dictionary.Types;
using ProjectPortfolio.CrossCutting.Exceptions;

namespace ProjectPortfolio.Application.Graph.Dictionary.Mutation
{
    [ExtendObjectType(Name = "Mutation")]
    public class DictionaryValueMutation
    {
        public async Task<AddDictionaryValuePayload> AddDictionaryValue(
            AddDictionaryValueInput input,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var addDictionaryValueCommand = new AddDictionaryValueCommand()
            {
                Name = input.Name,
                DictionaryKey = input.DictionaryKey,
                IsActive = input.IsActive,
                Code = input.Code,
                Sequence = input.Sequence
            };
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                try
                {
                    var response = await mediator.Send(addDictionaryValueCommand, cancellationToken);
                    return new AddDictionaryValuePayload(response);
                }
                catch (ValidationException e)
                {
                    var userErrors = new List<UserError>();
                    e.Message.Split(";").ForAll(item =>
                    {
                        userErrors.Add(new UserError(item, item));
                    });
                    return new AddDictionaryValuePayload(userErrors);
                }
            }
        }
    }
}