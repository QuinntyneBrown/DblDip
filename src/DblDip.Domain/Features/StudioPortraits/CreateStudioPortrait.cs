using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateStudioPortrait
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.StudioPortrait).NotNull();
                RuleFor(request => request.StudioPortrait).SetValidator(new StudioPortraitValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public StudioPortraitDto StudioPortrait { get; init; }
        }

        public class Response
        {
            public StudioPortraitDto StudioPortrait { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var studioPortrait = new StudioPortrait();

                _store.Add(studioPortrait);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    StudioPortrait = studioPortrait.ToDto()
                };
            }
        }
    }
}
