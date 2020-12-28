using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features.Posts
{
    public class RemovePost
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid PostId { get; init; }
        }

        public class Response
        {
            public PostDto Post { get; init; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IDateTime _dateTime;

            public Handler(IAppDbContext context, IDateTime dateTime)
            {
                _context = context;
                _dateTime = dateTime;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var post = await _context.FindAsync<Post>(request.PostId);

                post.Remove(_dateTime.UtcNow);

                _context.Store(post);

                await _context.SaveChangesAsync(cancellationToken);

                return new(); ;
            }
        }
    }
}
