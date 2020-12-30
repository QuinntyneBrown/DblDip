using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features.Epics
{
    public class RemoveEpic
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid EpicId { get; init; }
        }

        public class Response
        {
            public EpicDto Epic { get; init; }
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

                var epic = await _context.FindAsync<Epic>(request.EpicId);

                epic.Remove(_dateTime.UtcNow);

                _context.Store(epic);

                await _context.SaveChangesAsync(cancellationToken);

                return new();
            }
        }
    }
}
