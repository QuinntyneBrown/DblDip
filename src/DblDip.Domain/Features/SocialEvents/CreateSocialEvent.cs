using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.SocialEvents
{
    public class CreateSocialEvent
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.SocialEvent).NotNull();
                RuleFor(request => request.SocialEvent).SetValidator(new SocialEventValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public SocialEventDto SocialEvent { get; init; }
        }

        public class Response
        {
            public SocialEventDto SocialEvent { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var socialEvent = new SocialEvent();

                _context.Store(socialEvent);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    SocialEvent = socialEvent.ToDto()
                };
            }
        }
    }
}
