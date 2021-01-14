using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdatePost
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Post).NotNull();
                RuleFor(request => request.Post).SetValidator(new PostValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public PostDto Post { get; init; }
        }

        public class Response
        {
            public PostDto Post { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var post = await _store.FindAsync<Post>(request.Post.PostId);

                post.UpdateBody(request.Post.Body);

                post.UpdateTitle(request.Post.Title);

                _store.Add(post);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Post = post.ToDto()
                };
            }
        }
    }
}
