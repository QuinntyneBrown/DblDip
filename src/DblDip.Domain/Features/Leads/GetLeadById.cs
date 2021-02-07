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
    public class GetLeadById
    {
        public class Request : IRequest<Response>
        {
            public Guid LeadId { get; init; }
        }

        public class Response: ResponseBase
        {
            public LeadDto Lead { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var lead = await _context.FindAsync<Lead>(request.LeadId);

                return new Response()
                {
                    Lead = lead.ToDto()
                };
            }
        }
    }
}
