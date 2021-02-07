using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateEngagement
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Engagement).NotNull();
                RuleFor(request => request.Engagement).SetValidator(new EngagementValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public EngagementDto Engagement { get; init; }
        }

        public class Response: ResponseBase
        {
            public EngagementDto Engagement { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var engagement = new Engagement();

                _store.Add(engagement);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Engagement = engagement.ToDto()
                };
            }
        }
    }
}
