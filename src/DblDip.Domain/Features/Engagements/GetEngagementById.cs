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
    public class GetEngagementById
    {
        public class Request : IRequest<Response>
        {
            public Guid EngagementId { get; init; }
        }

        public class Response: ResponseBase
        {
            public EngagementDto Engagement { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var engagement = await _context.FindAsync<Engagement>(request.EngagementId);

                return new Response()
                {
                    Engagement = engagement.ToDto()
                };
            }
        }
    }
}
