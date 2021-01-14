using BuildingBlocks.EventStore;
using BuildingBlocks.EventStore;
using FluentValidation;
using MediatR;
using DblDip.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class RemoveLead
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid LeadId { get; init; }
        }

        public class Response
        {
            public LeadDto Lead { get; init; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IEventStore _store;
            private readonly IDateTime _dateTime;

            public Handler(IEventStore store, IDateTime dateTime) => (_store, _dateTime) = (store, dateTime);

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var lead = await _store.FindAsync<Lead>(request.LeadId);

                lead.Remove(_dateTime.UtcNow);

                _store.Add(lead);

                await _store.SaveChangesAsync(cancellationToken);

                return new Unit { };
            }
        }
    }
}
