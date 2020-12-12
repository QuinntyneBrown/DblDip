using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Services
{
    public class GetServiceById
    {
        public class Request : IRequest<Response>
        {
            public Guid ServiceId { get; set; }
        }

        public class Response
        {
            public ServiceDto Service { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var service = await _context.FindAsync<Service>(request.ServiceId);

                return new Response()
                {
                    Service = service.ToDto()
                };
            }
        }
    }
}
