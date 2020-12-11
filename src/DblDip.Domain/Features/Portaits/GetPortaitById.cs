using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Portraits
{
    public class GetPortraitById
    {
        public class Request : IRequest<Response>
        {
            public Guid PortraitId { get; set; }
        }

        public class Response
        {
            public PortraitDto Portrait { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var portrait = await _context.FindAsync<Portrait>(request.PortraitId);

                return new Response()
                {
                    Portrait = portrait.ToDto()
                };
            }
        }
    }
}
