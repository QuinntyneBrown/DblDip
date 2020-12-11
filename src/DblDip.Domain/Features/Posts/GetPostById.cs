using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Posts
{
    public class GetPostById
    {
        public class Request : IRequest<Response> {  
            public Guid PostId { get; set; }        
        }

        public class Response
        {
            public PostDto Post { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var post = await _context.FindAsync<Post>(request.PostId);

                return new Response() { 
                    Post = post.ToDto()
                };
            }
        }
    }
}
