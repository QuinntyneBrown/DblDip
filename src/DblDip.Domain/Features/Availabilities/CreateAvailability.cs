using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateAvailability
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Availability).NotNull();
                RuleFor(request => request.Availability).SetValidator(new AvailabilityValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public AvailabilityDto Availability { get; init; }
        }

        public class Response: ResponseBase
        {
            public AvailabilityDto Availability { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var availability = new Availability();

                _store.Add(availability);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Availability = availability.ToDto()
                };
            }
        }
    }
}
