using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateCorporateEvent
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.CorporateEvent).NotNull();
                RuleFor(request => request.CorporateEvent).SetValidator(new CorporateEventValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public CorporateEventDto CorporateEvent { get; init; }
        }

        public class Response
        {
            public CorporateEventDto CorporateEvent { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var corporateEvent = await _store.FindAsync<CorporateEvent>(request.CorporateEvent.CorporateEventId);

                corporateEvent.Update();

                _store.Add(corporateEvent);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    CorporateEvent = corporateEvent.ToDto()
                };
            }
        }
    }
}
