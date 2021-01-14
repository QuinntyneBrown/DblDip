using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateCard
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Card).NotNull();
                RuleFor(request => request.Card).SetValidator(new CardValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public CardDto Card { get; init; }
        }

        public class Response
        {
            public CardDto Card { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var card = await _store.FindAsync<Card>(request.Card.CardId);

                card.Update();

                _store.Add(card);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Card = card.ToDto()
                };
            }
        }
    }
}
