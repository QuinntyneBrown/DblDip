using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetSocialEventById
    {
        public class Request : IRequest<Response>
        {
            public Guid SocialEventId { get; init; }
        }

        public class Response
        {
            public SocialEventDto SocialEvent { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var socialEvent = await _context.FindAsync<SocialEvent>(request.SocialEventId);

                return new Response()
                {
                    SocialEvent = socialEvent.ToDto()
                };
            }
        }
    }
}
