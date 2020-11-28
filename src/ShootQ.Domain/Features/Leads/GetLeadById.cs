using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Leads
{
    public class GetLeadById
    {
        public class Request : IRequest<Response> {  
            public Guid LeadId { get; set; }        
        }

        public class Response
        {
            public LeadDto Lead { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var lead = await _context.FindAsync<Lead>(request.LeadId);

                return new Response() { 
                    Lead = lead.ToDto()
                };
            }
        }
    }
}
