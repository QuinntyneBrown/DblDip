using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateDashboardCards
    {
        public record Request(Guid DashboardId, ICollection<DashboardCardDto> DashboardCards) : IRequest<Response>;

        public record Response(DashboardDto Dashboard);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store)
                => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dashboard = await _store.FindAsync<Dashboard>(request.DashboardId);

                var dashboardCards = request.DashboardCards.Select(x => new DashboardCard(x.DashboardCardId, x.Options)).ToList();

                dashboard.UpdateDashboardCards(dashboardCards);

                _store.Add(dashboard);

                await _store.SaveChangesAsync(cancellationToken);

                return new (dashboard.ToDto());
            }
        }
    }
}
