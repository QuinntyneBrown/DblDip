using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DblDip.Core.ValueObjects;
using DblDip.Domain.IntegrationEvents;
using BuildingBlocks.EventStore;

namespace DblDip.Domain.Features
{
    public class CreateClient
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Response>
        {
            public string Name { get; set; }
            public Email Email { get; set; }
        }

        public class Response: ResponseBase
        {
            public ClientDto Client { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;
            private readonly IMediator _mediator;

            public Handler(IEventStore store, IMediator mediator)
            {
                _store = store;
                _mediator = mediator;
            }


            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var client = new Client(request.Name, request.Email);

                _store.Add(client);

                await _store.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(new ProfileCreated(client));

                return new Response()
                {
                    Client = client.ToDto()
                };
            }
        }
    }
}
