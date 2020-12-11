using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Blogs
{
    public class GetBlogById
    {
        public class Request : IRequest<Response> {  
            public Guid BlogId { get; set; }        
        }

        public class Response
        {
            public BlogDto Blog { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var blog = await _context.FindAsync<Blog>(request.BlogId);

                return new Response() { 
                    Blog = blog.ToDto()
                };
            }
        }
    }
}
