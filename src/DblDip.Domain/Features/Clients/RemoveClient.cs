using BuildingBlocks.EventStore;
using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using DblDip.Core.Data;

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
            private readonly IEventStore _context;
            private readonly IDateTime _dateTime;

            public Handler(IEventStore context, IDateTime dateTime)
            {
                _context = context;
                _dateTime = dateTime;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var client = await _context.LoadAsync<Client>(request.ClientId);

                client.Remove(_dateTime.UtcNow);

                await _context.SaveChangesAsync(cancellationToken);

                return new();
            }
        }
    }
}
