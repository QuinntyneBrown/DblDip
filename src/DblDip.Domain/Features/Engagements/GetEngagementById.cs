using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Engagements
{
    public class GetEngagementById
    {
        public class Request : IRequest<Response>
        {
            public Guid EngagementId { get; init; }
        }

        public class Response
        {
            public EngagementDto Engagement { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

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
