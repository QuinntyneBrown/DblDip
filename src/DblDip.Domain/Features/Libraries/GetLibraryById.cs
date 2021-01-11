using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetLibraryById
    {
        public class Request : IRequest<Response>
        {
            public Guid LibraryId { get; init; }
        }

        public class Response
        {
            public LibraryDto Library { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var library = await _context.FindAsync<Library>(request.LibraryId);

                return new Response()
                {
                    Library = library.ToDto()
                };
            }
        }
    }
}
