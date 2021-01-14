using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateVenue
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Venue).NotNull();
                RuleFor(request => request.Venue).SetValidator(new VenueValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public VenueDto Venue { get; init; }
        }

        public class Response
        {
            public VenueDto Venue { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var venue = await _store.FindAsync<Venue>(request.Venue.VenueId);

                venue.Update();

                _store.Add(venue);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Venue = venue.ToDto()
                };
            }
        }
    }
}
