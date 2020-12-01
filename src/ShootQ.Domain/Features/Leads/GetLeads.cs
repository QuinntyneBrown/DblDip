using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Leads
{
    public class GetLeads
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public List<LeadDto> Leads { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new Response()
                {
                    Leads = _context.Set<Lead>().Select(x => x.ToDto()).ToList()
                };
            }
        }
    }
}
