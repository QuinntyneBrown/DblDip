using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Venues
{
    public class GetVenueById
    {
        public class Request : IRequest<Response> {  
            public Guid VenueId { get; set; }        
        }

        public class Response
        {
            public VenueDto Venue { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var venue = await _context.FindAsync<Venue>(request.VenueId);

                return new Response() { 
                    Venue = venue.ToDto()
                };
            }
        }
    }
}
