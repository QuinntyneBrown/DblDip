using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Meetings
{
    public class GetMeetings
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public List<MeetingDto> Meetings { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new Response()
                {
                    Meetings = _context.Set<Meeting>().Select(x => x.ToDto()).ToList()
                };
            }
        }
    }
}
