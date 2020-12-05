using FluentValidation;
using MediatR;
using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;
using ShootQ.Core.ValueObjects;

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
        }

        public class Response
        {
            public Price Total { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) {            
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var wedding = await _context.FindAsync<Wedding>(request.WeddingId);

                var total = 0m;

                foreach(var part in wedding.Parts)
                {
                    var rate = await _context.FindAsync<PhotographyRate>(part.PhotographyRateId);

                    total += part.dateRange.Hours * rate.Price;
                }
                return new Response()
                {
                    Total = (Price)total
                };
            }
        }
    }
}
