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
    public class QuoteWedding
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
            public Price Total { get; set; }
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

                var wedding = await _context.FindAsync<Wedding>(request.WeddingId);

                var total = 0m;

                foreach(var part in wedding.Parts)
                {
                    var rate = await _context.FindAsync<PhotographyRate>(part.PhotographyRateId);

                    total += part.DateRange.Hours * rate.Price;
                }

                wedding.AddQuote((Email)request.Email, (Price)total, _dateTime.UtcNow);

                _context.Store(wedding);

                await _context.SaveChangesAsync(default);

                return new Response()
                {
                    Total = (Price)total
                };
            }
        }
    }
}
