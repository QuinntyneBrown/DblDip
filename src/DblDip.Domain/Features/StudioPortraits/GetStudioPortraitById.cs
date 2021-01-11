using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetStudioPortraitById
    {
        public class Request : IRequest<Response>
        {
            public Guid StudioPortraitId { get; init; }
        }

        public class Response
        {
            public StudioPortraitDto StudioPortrait { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var studioPortrait = await _context.FindAsync<StudioPortrait>(request.StudioPortraitId);

                return new Response()
                {
                    StudioPortrait = studioPortrait.ToDto()
                };
            }
        }
    }
}
