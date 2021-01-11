using DblDip.Core.Data;
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
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var card = await _context.FindAsync<Card>(request.Card.CardId);

                card.Update();

                _context.Add(card);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Card = card.ToDto()
                };
            }
        }
    }
}
