using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features.WeddingQuotes
{
    public class RemoveWeddingQuote
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid WeddingQuoteId { get; set; }
        }

        public class Response
        {
            public WeddingQuoteDto WeddingQuote { get; set; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var weddingQuote = await _context.FindAsync<WeddingQuote>(request.WeddingQuoteId);

                //weddingQuote.Remove();

                _context.Store(weddingQuote);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit()
                {

                };
            }
        }
    }
}
