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
    public class GetPointById
    {
        public class Request : IRequest<Response>
        {
            public Guid PointId { get; init; }
        }

        public class Response: ResponseBase
        {
            public PointDto Point { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var point = await _context.FindAsync<Point>(request.PointId);

                return new Response()
                {
                    Point = point.ToDto()
                };
            }
        }
    }
}
