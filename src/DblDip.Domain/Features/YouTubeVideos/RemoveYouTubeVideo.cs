using BuildingBlocks.EventStore;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features
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
            private readonly IEventStore _store;
            private readonly IDateTime _dateTime;

            public Handler(IEventStore store, IDateTime dateTime) => (_store, _dateTime) = (store, dateTime);


            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var youTubeVideo = await _store.FindAsync<YouTubeVideo>(request.YouTubeVideoId);

                youTubeVideo.Remove(_dateTime.UtcNow);

                _store.Add(youTubeVideo);

                await _store.SaveChangesAsync(cancellationToken);

                return new()
                {

                };
            }
        }
    }
}
