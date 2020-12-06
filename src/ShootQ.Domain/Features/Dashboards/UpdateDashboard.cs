using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Dashboards
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

        public class Request : IRequest<Response> {  
            public DashboardDto Dashboard { get; set; }
        }

        public class Response
        {
            public DashboardDto Dashboard { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var dashboard = await _context.FindAsync<Dashboard>(request.Dashboard.DashboardId);

                dashboard.Update(request.Dashboard.Name);

                _context.Store(dashboard);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Dashboard = dashboard.ToDto()
                };
            }
        }
    }
}
