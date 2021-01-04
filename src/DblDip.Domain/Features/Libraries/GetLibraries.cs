using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetLibraries
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public List<LibraryDto> Libraries { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new Response()
                {
                    Libraries = _context.Set<Library>().Select(x => x.ToDto()).ToList()
                };
            }
        }
    }
}
