using BuildingBlocks.Core;
using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetParticipantById
    {
        public class Request : IRequest<Response>
        {
            public Guid ParticipantId { get; init; }
        }

        public class Response: ResponseBase
        {
            public ParticipantDto Participant { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var participant = await _context.FindAsync<Participant>(request.ParticipantId);

                return new Response()
                {
                    Participant = participant.ToDto()
                };
            }
        }
    }
}
