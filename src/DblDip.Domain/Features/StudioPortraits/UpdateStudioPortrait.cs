using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateStudioPortrait
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

        public class Response: ResponseBase
        {
            public StudioPortraitDto StudioPortrait { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var studioPortrait = await _store.FindAsync<StudioPortrait>(request.StudioPortrait.StudioPortraitId);

                studioPortrait.Update();

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
