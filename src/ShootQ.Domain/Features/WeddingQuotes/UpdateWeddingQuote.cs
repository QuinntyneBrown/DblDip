using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.WeddingQuotes
{
    public class UpdateWeddingQuote
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.WeddingQuote).NotNull();
                RuleFor(request => request.WeddingQuote).SetValidator(new WeddingQuoteValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public WeddingQuoteDto WeddingQuote { get; set; }
        }

        public class Response
        {
            public WeddingQuoteDto WeddingQuote { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var weddingQuote = await _context.FindAsync<WeddingQuote>(request.WeddingQuote.WeddingQuoteId);

                //weddingQuote.Update();

                _context.Store(weddingQuote);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    WeddingQuote = weddingQuote.ToDto()
                };
            }
        }
    }
}
