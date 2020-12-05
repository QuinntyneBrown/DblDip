using BuildingBlocks.Abstractions;
using FluentValidation;
using MediatR;
using ShootQ.Core.Models;
using ShootQ.Core.ValueObjects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Weddings
{
    public class CreateWeddingQuote
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Response> {
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

            public Handler(IAppDbContext context, IDateTime dateTime) {            
                _context = context;
                _dateTime = dateTime;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var quote = new WeddingQuote((Email)request.Email, request.WeddingId);

                var wedding = await _context.FindAsync<Wedding>(request.WeddingId);

                foreach(var part in wedding.Parts)
                {
                    var rate = await _context.FindAsync<PhotographyRate>(part.PhotographyRateId);

                    quote.AddItem((Price)(part.DateRange.Hours * rate.Price), "Wedding Photography");
                }

                foreach (var trip in wedding.Trips)
                {                    
                    quote.AddItem((Price)(trip.DateRange.Hours * 60), "Travel Expense");
                }

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
