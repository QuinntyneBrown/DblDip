using BuildingBlocks.Abstractions;
using MediatR;
using ShootQ.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static ShootQ.Core.Models.Dashboard;

namespace ShootQ.Domain.Features.Dashboards
{
    public class UpdateDashboardCards
    {
        public record Request(Guid DashboardId, IEnumerable<DashboardCardDto> DashboardCards) : IRequest<Response>;

        public record Response(DashboardDto Dashboard);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context)
                => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dashboard = await _context.FindAsync<Dashboard>(request.DashboardId);

                var dashboardCards = request.DashboardCards.Select(x => new DashboardCard(x.DashboardCardId, x.Options));

                dashboard.UpdateDashboardCards(dashboardCards);

                _context.Store(dashboard);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response(dashboard.ToDto());
            }
        }
    }
}
