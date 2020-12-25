using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Availabilities
{
    public class GetAvailabilityById
    {
        public class Request : IRequest<Response>
        {
            public Guid AvailabilityId { get; init; }
        }

        public class Response
        {
            public AvailabilityDto Availability { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var availability = await _context.FindAsync<Availability>(request.AvailabilityId);

                return new Response()
                {
                    Availability = availability.ToDto()
                };
            }
        }
    }
}
