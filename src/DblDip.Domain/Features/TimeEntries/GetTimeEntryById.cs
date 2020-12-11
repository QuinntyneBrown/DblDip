using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.TimeEntries
{
    public class GetTimeEntryById
    {
        public class Request : IRequest<Response> {  
            public Guid TimeEntryId { get; set; }        
        }

        public class Response
        {
            public TimeEntryDto TimeEntry { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var timeEntry = await _context.FindAsync<TimeEntry>(request.TimeEntryId);

                return new Response() { 
                    TimeEntry = timeEntry.ToDto()
                };
            }
        }
    }
}
