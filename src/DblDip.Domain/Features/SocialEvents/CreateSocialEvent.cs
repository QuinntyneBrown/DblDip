using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
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
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var socialEvent = new SocialEvent();

                _store.Add(socialEvent);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    SocialEvent = socialEvent.ToDto()
                };
            }
        }
    }
}
