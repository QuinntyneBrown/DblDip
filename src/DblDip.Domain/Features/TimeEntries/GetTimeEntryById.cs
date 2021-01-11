using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetTimeEntryById
    {
        public class Request : IRequest<Response>
        {
            public Guid TimeEntryId { get; init; }
        }

        public class Response
        {
            public TimeEntryDto TimeEntry { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var timeEntry = await _context.FindAsync<TimeEntry>(request.TimeEntryId);

                return new Response()
                {
                    TimeEntry = timeEntry.ToDto()
                };
            }
        }
    }
}
