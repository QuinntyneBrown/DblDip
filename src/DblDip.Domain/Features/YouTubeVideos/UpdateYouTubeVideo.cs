using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
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

        public class Request : IRequest<Response>
        {
            public YouTubeVideoDto YouTubeVideo { get; init; }
        }

        public class Response
        {
            public YouTubeVideoDto YouTubeVideo { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var youTubeVideo = await _context.FindAsync<YouTubeVideo>(request.YouTubeVideo.YouTubeVideoId);

                youTubeVideo.Update();

                _context.Add(youTubeVideo);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    YouTubeVideo = youTubeVideo.ToDto()
                };
            }
        }
    }
}
