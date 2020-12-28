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
            public Guid YouTubeVideoId { get; init; }
        }

        public class Response
        {
            public YouTubeVideoDto YouTubeVideo { get; init; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IDateTime _dateTime;

            public Handler(IAppDbContext context, IDateTime dateTime) => (_context, _dateTime) = (context, dateTime);


            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var youTubeVideo = await _context.FindAsync<YouTubeVideo>(request.YouTubeVideoId);

                youTubeVideo.Remove(_dateTime.UtcNow);

                _context.Store(youTubeVideo);

                await _context.SaveChangesAsync(cancellationToken);

                return new()
                {

                };
            }
        }
    }
}
