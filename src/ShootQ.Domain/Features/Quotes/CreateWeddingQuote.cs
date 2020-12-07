using BuildingBlocks.Abstractions;
using FluentValidation;
using MediatR;
using ShootQ.Core.Models;
using ShootQ.Core.ValueObjects;
using System;
using System.Threading;
using System.Threading.Tasks;
using static ShootQ.Core.Constants.Rates;

namespace ShootQ.Domain.Features.Quotes
{
    public class CreateWeddingQuote
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Response>
        {
            public Guid WeddingId { get; set; }
            public string Email { get; set; }
        }

        public class Response
        {
            public QuoteDto Quote { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly IDateTime _dateTime;

            public Handler(IAppDbContext context, IDateTime dateTime)
            {
                _context = context;
                _dateTime = dateTime;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var wedding = await _context.FindAsync<Wedding>(request.WeddingId);

                var rate = await _context.FindAsync<Rate>(PhotographyRate);

                var quote = new WeddingQuote((Email)request.Email, wedding, rate);

                _context.Store(quote);

                await _context.SaveChangesAsync(default);

                return new Response()
                {
                    Quote = quote.ToDto()
                };
            }
        }
    }
}
