using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using ShootQ.Core.ValueObjects;

namespace ShootQ.Domain.Features.Weddings
{
    public class CreateWedding
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Response> {
            public Guid CustomerId { get; set; }
            public int Hours { get; set; }
            public Guid PhotographyRateId { get; set; }
            public DateTime DateTime { get; set; }
            public Location Location { get; set; }
        }

        public class Response
        {
            public WeddingDto Wedding { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var wedding = new Wedding(request.Location, request.DateTime, request.Hours, request.PhotographyRateId);

                _context.Store(wedding);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Wedding = wedding.ToDto()
                };
            }
        }
    }
}
