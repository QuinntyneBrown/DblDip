using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Meetings
{
    public class GetMeetingById
    {
        public class Request : IRequest<Response> {  
            public Guid MeetingId { get; set; }        
        }

        public class Response
        {
            public MeetingDto Meeting { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var meeting = await _context.FindAsync<Meeting>(request.MeetingId);

                return new Response() { 
                    Meeting = meeting.ToDto()
                };
            }
        }
    }
}
