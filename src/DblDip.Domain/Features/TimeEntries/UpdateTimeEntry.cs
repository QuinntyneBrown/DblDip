using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.TimeEntries
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
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var timeEntry = await _context.FindAsync<TimeEntry>(request.TimeEntry.TimeEntryId);

                //timeEntry.Update();

                _context.Store(timeEntry);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    TimeEntry = timeEntry.ToDto()
                };
            }
        }
    }
}
