using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features.Dashboards
{
    public class RemoveDashboard
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid DashboardId { get; init; }
        }

        public class Response
        {
            public DashboardDto Dashboard { get; init; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IDateTime _dateTime;

            public Handler(IAppDbContext context, IDateTime dateTime) => (_context, _dateTime) = (context, dateTime);

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var dashboard = await _context.FindAsync<Dashboard>(request.DashboardId);

                dashboard.Remove(_dateTime.UtcNow);

                _context.Store(dashboard);

                await _context.SaveChangesAsync(cancellationToken);

                return new ()
                {

                };
            }
        }
    }
}
