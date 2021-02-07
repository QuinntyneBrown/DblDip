using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdatePortrait
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Portrait).NotNull();
                RuleFor(request => request.Portrait).SetValidator(new PortraitValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public PortraitDto Portrait { get; init; }
        }

        public class Response: ResponseBase
        {
            public PortraitDto Portrait { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var portrait = await _store.FindAsync<Portrait>(request.Portrait.PortraitId);

                portrait.Update();

                _store.Add(portrait);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Portrait = portrait.ToDto()
                };
            }
        }
    }
}
