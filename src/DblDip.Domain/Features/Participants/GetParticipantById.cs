using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Participants
{
    public class GetParticipantById
    {
        public class Request : IRequest<Response> {  
            public Guid ParticipantId { get; set; }        
        }

        public class Response
        {
            public ParticipantDto Participant { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var participant = await _context.FindAsync<Participant>(request.ParticipantId);

                return new Response() { 
                    Participant = participant.ToDto()
                };
            }
        }
    }
}
