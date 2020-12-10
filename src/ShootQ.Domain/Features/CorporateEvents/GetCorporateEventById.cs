using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.CorporateEvents
{
    public class GetCorporateEventById
    {
        public class Request : IRequest<Response> {  
            public Guid CorporateEventId { get; set; }        
        }

        public class Response
        {
            public CorporateEventDto CorporateEvent { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var corporateEvent = await _context.FindAsync<CorporateEvent>(request.CorporateEventId);

                return new Response() { 
                    CorporateEvent = corporateEvent.ToDto()
                };
            }
        }
    }
}
