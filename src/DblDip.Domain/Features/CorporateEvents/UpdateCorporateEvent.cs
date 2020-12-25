using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.CorporateEvents
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
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var corporateEvent = await _context.FindAsync<CorporateEvent>(request.CorporateEvent.CorporateEventId);

                //corporateEvent.Update();

                _context.Store(corporateEvent);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    CorporateEvent = corporateEvent.ToDto()
                };
            }
        }
    }
}
