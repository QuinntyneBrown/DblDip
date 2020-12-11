using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.YouTubeVideos
{
    public class GetYouTubeVideoById
    {
        public class Request : IRequest<Response> {  
            public Guid YouTubeVideoId { get; set; }        
        }

        public class Response
        {
            public YouTubeVideoDto YouTubeVideo { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var youTubeVideo = await _context.FindAsync<YouTubeVideo>(request.YouTubeVideoId);

                return new Response() { 
                    YouTubeVideo = youTubeVideo.ToDto()
                };
            }
        }
    }
}