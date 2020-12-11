using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.YouTubeVideos
{
    public class UpdateYouTubeVideo
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.YouTubeVideo).NotNull();
                RuleFor(request => request.YouTubeVideo).SetValidator(new YouTubeVideoValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public YouTubeVideoDto YouTubeVideo { get; set; }
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

                var youTubeVideo = await _context.FindAsync<YouTubeVideo>(request.YouTubeVideo.YouTubeVideoId);

                youTubeVideo.Update(default);

                _context.Store(youTubeVideo);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    YouTubeVideo = youTubeVideo.ToDto()
                };
            }
        }
    }
}
