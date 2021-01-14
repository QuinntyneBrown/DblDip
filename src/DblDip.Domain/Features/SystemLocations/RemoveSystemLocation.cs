using BuildingBlocks.EventStore;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features
{
    public class RemoveSystemLocation
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid SystemLocationId { get; init; }
        }

        public class Response
        {
            public SystemLocationDto SystemLocation { get; init; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IEventStore _store;
            private readonly IDateTime _dateTime;

            public Handler(IEventStore store, IDateTime dateTime) => (_store, _dateTime) = (store, dateTime);

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var systemLocation = await _store.FindAsync<SystemLocation>(request.SystemLocationId);

                systemLocation.Remove(_dateTime.UtcNow);

                _store.Add(systemLocation);

                await _store.SaveChangesAsync(cancellationToken);

                return new()
                {

                };
            }
        }
    }
}
