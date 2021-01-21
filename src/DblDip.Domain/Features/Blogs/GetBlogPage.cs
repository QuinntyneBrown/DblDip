using DblDip.Core.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetBlogPage
    {
        public class Request : IRequest<Response> {
            public Guid BlogId { get; set; }
            public int PageSize { get; init; } = 10;
            public int Page { get; init; } = 1;
        }

        public class Response
        {
            public BlogPageDto BlogPage { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context){
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var query = from b in _context.Blogs
                            join a in _context.Blogs.SelectMany(x => x.Posts) on true equals true
                            join p in _context.Posts on a.PostId equals p.PostId
                            where b.BlogId == request.BlogId && !p.Deleted.HasValue
                            select p;

                var count = (await _context.Blogs.Include(x => x.Posts).SingleAsync(x => x.BlogId == request.BlogId)).Posts.Count();

                var posts = await query.Skip(request.PageSize * (request.Page - 1)).Take(request.PageSize).Select(x => x.ToDto()).ToListAsync();

                return new Response() { 
                    BlogPage = new()
                    {
                        TotalPages = ((int)(count / request.PageSize)) + ((count % request.PageSize) > 0 ? 1 : 0),
                        CurrentPage = request.Page,                        
                        TotalResults = count,
                        Posts = posts 
                    }
                };
            }
        }
    }
}
