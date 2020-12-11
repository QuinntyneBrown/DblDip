using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Points
{
    public class GetPoints
    {
        public class Request : IRequest<Response> {  }

        public class Response
        {
            public List<PointDto> Points { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
			    return new Response() { 
                    Points = _context.Set<Point>().Select(x => x.ToDto()).ToList()
                };
            }
        }
    }
}
