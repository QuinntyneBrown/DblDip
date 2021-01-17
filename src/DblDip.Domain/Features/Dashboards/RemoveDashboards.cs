using BuildingBlocks.EventStore;
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

            public Handler(IEventStore store, IDateTime dateTime) {
                _store = store;
                _dateTime = dateTime;
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
