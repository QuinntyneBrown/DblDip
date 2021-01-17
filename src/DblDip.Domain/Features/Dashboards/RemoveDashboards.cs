using BuildingBlocks.EventStore;
using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class RemoveDashboards
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public record Request(IEnumerable<Guid> DashboardIds) : IRequest<Unit>;


        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IEventStore _store;
            private readonly IDateTime _dateTime;
            private readonly IDblDipDbContext _context;

            public Handler(IEventStore store, IDateTime dateTime, IDblDipDbContext context) {
                _store = store;
                _dateTime = dateTime;
                _context = context;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken) {

                foreach(var id in request.DashboardIds)
                {
                    var dashboard = await _store.FindAsync<Dashboard>(id);

                    dashboard.Remove(_dateTime.UtcNow);
                }

                await _store.SaveChangesAsync(cancellationToken);

                return new();
            }
        }
    }
}
