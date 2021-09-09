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
    public class DictionaryMutation
    {
        public async Task<AddDictionaryPayload> AddDictionary(
            AddDictionaryInput input,
            [Service] IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            var addDictionaryCommand = new AddDictionaryCommand()
            {
                Name = input.Name,
                DictionaryKey = input.DictionaryKey
            };
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                try
                {
                    var response = await mediator.Send(addDictionaryCommand, cancellationToken);
                    return new AddDictionaryPayload(response);
                }
                catch (ValidationException e)
                {
                    var userErrors = new List<UserError>();
                    e.Message.Split(";").ForAll(item =>
                    {
                        userErrors.Add(new UserError(item, item));
                    });
                    return new AddDictionaryPayload(userErrors);
                }
            }
        }
    }
}