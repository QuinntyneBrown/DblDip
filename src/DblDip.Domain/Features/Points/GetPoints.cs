using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetPoints
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public List<PointDto> Points { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new Response()
                {
                    Points = _context.Set<Point>().Select(x => x.ToDto()).ToList()
                };
            }
        }
    }
}
