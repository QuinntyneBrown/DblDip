using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
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

        public class Response: ResponseBase
        {
            public BlogDto Blog { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var blog = await _store.FindAsync<Blog>(request.Blog.BlogId);

                blog.Update();

                _store.Add(blog);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Blog = blog.ToDto()
                };
            }
        }
    }
}
