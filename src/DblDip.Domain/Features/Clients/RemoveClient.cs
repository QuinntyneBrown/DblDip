using BuildingBlocks.EventStore;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using BuildingBlocks.EventStore;

namespace DblDip.Domain.Features
{
    public class RemoveClient
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.ClientId).NotNull();
                RuleFor(x => x.ClientId).NotEqual(new Guid());
            }
        }

        public record Request(Guid ClientId) : IRequest<Unit>;

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IEventStore _store;
            private readonly IDateTime _dateTime;

            public Handler(IEventStore store, IDateTime dateTime)
            {
                _store = store;
                _dateTime = dateTime;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var client = await _store.FindAsync<Client>(request.ClientId);

                client.Remove(_dateTime.UtcNow);

                await _store.SaveChangesAsync(cancellationToken);

                return new();
            }
        }
    }
}
