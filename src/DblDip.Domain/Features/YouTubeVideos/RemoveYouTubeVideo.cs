using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features.YouTubeVideos
{
    public class RemoveYouTubeVideo
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid YouTubeVideoId { get; set; }
        }

        public class Response
        {
            public YouTubeVideoDto YouTubeVideo { get; set; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var youTubeVideo = await _context.FindAsync<YouTubeVideo>(request.YouTubeVideoId);

                youTubeVideo.Remove(default);

                _context.Store(youTubeVideo);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit()
                {

                };
            }
        }
    }
}
