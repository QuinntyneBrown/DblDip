using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateMeeting
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Meeting).NotNull();
                RuleFor(request => request.Meeting).SetValidator(new MeetingValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public MeetingDto Meeting { get; init; }
        }

        public class Response: ResponseBase
        {
            public MeetingDto Meeting { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var meeting = new Meeting();

                _store.Add(meeting);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Meeting = meeting.ToDto()
                };
            }
        }
    }
}
