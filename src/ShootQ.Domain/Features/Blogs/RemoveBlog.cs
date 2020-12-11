using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ShootQ.Domain.Features.Blogs
{
    public class RemoveBlog
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit> {  
            public Guid BlogId { get; set; }
        }

        public class Response
        {
            public BlogDto Blog { get; set; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken) {

                var blog = await _context.FindAsync<Blog>(request.BlogId);

                //blog.Remove();

                _context.Store(blog);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit()
                {

                };
            }
        }
    }
}
