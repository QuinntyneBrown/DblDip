using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateBlog
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Blog).NotNull();
                RuleFor(request => request.Blog).SetValidator(new BlogValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public BlogDto Blog { get; init; }
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

                var blog = await _context.FindAsync<Blog>(request.Blog.BlogId);

                blog.Update();

                _context.Store(blog);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Blog = blog.ToDto()
                };
            }
        }
    }
}
