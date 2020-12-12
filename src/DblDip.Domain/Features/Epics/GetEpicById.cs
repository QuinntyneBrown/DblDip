using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Epics
{
    public class GetEpicById
    {
        public class Request : IRequest<Response>
        {
            public Guid EpicId { get; set; }
        }

        public class Response
        {
            public EpicDto Epic { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var epic = await _context.FindAsync<Epic>(request.EpicId);

                return new Response()
                {
                    Epic = epic.ToDto()
                };
            }
        }
    }
}
