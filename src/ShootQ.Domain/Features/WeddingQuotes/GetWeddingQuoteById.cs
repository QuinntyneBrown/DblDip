using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.WeddingQuotes
{
    public class GetWeddingQuoteById
    {
        public class Request : IRequest<Response> {  
            public Guid WeddingQuoteId { get; set; }        
        }

        public class Response
        {
            public WeddingQuoteDto WeddingQuote { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var weddingQuote = await _context.FindAsync<WeddingQuote>(request.WeddingQuoteId);

                return new Response() { 
                    WeddingQuote = weddingQuote.ToDto()
                };
            }
        }
    }
}
