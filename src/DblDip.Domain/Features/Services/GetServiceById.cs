using BuildingBlocks.Core;
using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetServiceById
    {
        public class Request : IRequest<Response>
        {
            public Guid ServiceId { get; init; }
        }

        public class Response: ResponseBase
        {
            public ServiceDto Service { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

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
