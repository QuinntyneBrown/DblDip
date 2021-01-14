using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateClient
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Client).NotNull();
                RuleFor(request => request.Client).SetValidator(new ClientValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public ClientDto Client { get; init; }
        }

        public class Response
        {
            public ClientDto Client { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var client = await _store.FindAsync<Client>(request.Client.ClientId);

                client.Update();

                _store.Add(client);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Client = client.ToDto()
                };
            }
        }
    }
}
