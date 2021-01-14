using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateCard
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Card).NotNull();
                RuleFor(request => request.Card).SetValidator(new CardValidator());
            }
        }

        public record Request(CardDto Card) : IRequest<Response>;

        public record Response(CardDto Card);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var card = new Card(request.Card.Name, request.Card.Description);

                _store.Add(card);

                await _store.SaveChangesAsync(cancellationToken);

                return new (card.ToDto());
            }
        }
    }
}
