using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Dashboards
{
    public class CreateDashboard
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Dashboard).NotNull();
                RuleFor(request => request.Dashboard).SetValidator(new DashboardValidator());
            }
        }

        public record Request(DashboardDto Dashboard) : IRequest<Response>;

        public record Response(DashboardDto Dashboard);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var dashboard = new Dashboard(request.Dashboard.Name, request.Dashboard.ProfileId);

                _context.Store(dashboard);

                await _context.SaveChangesAsync(cancellationToken);

                return new(dashboard.ToDto());
            }
        }
    }
}
