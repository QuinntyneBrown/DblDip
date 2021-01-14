using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateDashboard
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Dashboard).NotNull();
                RuleFor(request => request.Dashboard).SetValidator(new DashboardValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public DashboardDto Dashboard { get; init; }
        }

        public class Response
        {
            public DashboardDto Dashboard { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dashboard = await _store.FindAsync<Dashboard>(request.Dashboard.DashboardId);

                dashboard.Update(request.Dashboard.Name);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Dashboard = dashboard.ToDto()
                };
            }
        }
    }
}
