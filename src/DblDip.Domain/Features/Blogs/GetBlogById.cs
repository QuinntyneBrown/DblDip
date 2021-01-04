using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetBlogById
    {
        public class Request : IRequest<Response>
        {
            public Guid BlogId { get; init; }
        }

        public class Response
        {
            public BlogDto Blog { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var blog = await _context.FindAsync<Blog>(request.BlogId);

                return new Response()
                {
                    Blog = blog.ToDto()
                };
            }
        }
    }
}
