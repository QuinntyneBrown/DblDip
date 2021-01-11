using BuildingBlocks.EventStore;
using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features
{
    public class RemoveShotList
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid ShotListId { get; init; }
        }

        public class Response
        {
            public ShotListDto ShotList { get; init; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IDblDipDbContext _context;
            private readonly IDateTime _dateTime;

            public Handler(IDblDipDbContext context, IDateTime dateTime)
            {
                _context = context;
                _dateTime = dateTime;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var shotList = await _context.FindAsync<ShotList>(request.ShotListId);

                shotList.Remove(_dateTime.UtcNow);

                _context.Add(shotList);

                await _context.SaveChangesAsync(cancellationToken);

                return new();
            }
        }
    }
}
