using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.SystemLocations
{
    public class GetSystemLocationById
    {
        public class Request : IRequest<Response>
        {
            public Guid SystemLocationId { get; set; }
        }

        public class Response
        {
            public SystemLocationDto SystemLocation { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var systemLocation = await _context.FindAsync<SystemLocation>(request.SystemLocationId);

                return new Response()
                {
                    SystemLocation = systemLocation.ToDto()
                };
            }
        }
    }
}
