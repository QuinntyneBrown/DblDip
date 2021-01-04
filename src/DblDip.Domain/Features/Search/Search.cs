using BuildingBlocks.Abstractions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class Search
    {
        public record Request(string Query) : IRequest<Response>;

        public record SearchResult();

        public record Response(ICollection<SearchResult> Results);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                return new Response(null);
            }
        }
    }
}
