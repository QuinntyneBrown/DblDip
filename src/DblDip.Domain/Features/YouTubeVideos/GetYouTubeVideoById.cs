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
    public class GetYouTubeVideoById
    {
        public class Request : IRequest<Response>
        {
            public Guid YouTubeVideoId { get; init; }
        }

        public class Response: ResponseBase
        {
            public YouTubeVideoDto YouTubeVideo { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var youTubeVideo = await _context.FindAsync<YouTubeVideo>(request.YouTubeVideoId);

                return new Response()
                {
                    YouTubeVideo = youTubeVideo.ToDto()
                };
            }
        }
    }
}
