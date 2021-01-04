using FluentValidation;
using MediatR;
using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace DblDip.Domain.Features
{
    public class CompleteTask
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit> {
            public Guid TaskId { get; init; }
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

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken) {

                var task = await _context.FindAsync<DblDip.Core.Models.Task>(request.TaskId);

                task.Complete(_dateTime.UtcNow);

                _context.Store(task);

                await _context.SaveChangesAsync(cancellationToken);

                return new();
            }
        }
    }
}
