using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateTimeEntry
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.TimeEntry).NotNull();
                RuleFor(request => request.TimeEntry).SetValidator(new TimeEntryValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public TimeEntryDto TimeEntry { get; init; }
        }

        public class Response
        {
            public TimeEntryDto TimeEntry { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var timeEntry = await _store.FindAsync<TimeEntry>(request.TimeEntry.TimeEntryId);

                timeEntry.Update();

                _store.Add(timeEntry);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    TimeEntry = timeEntry.ToDto()
                };
            }
        }
    }
}
