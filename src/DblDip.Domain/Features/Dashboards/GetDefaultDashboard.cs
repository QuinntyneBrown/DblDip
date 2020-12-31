using BuildingBlocks.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using DblDip.Core;
using DblDip.Core.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Dashboards
{
    public class GetDefaultDashboard
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public DashboardDto Dashboard { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IAppDbContext context, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var currentUserId = new Guid(_httpContextAccessor.HttpContext.User
                    .FindFirst(Constants.ClaimTypes.UserId).Value);

                var dashboard = _context.Set<Dashboard>().Single(x => x.ProfileId == currentUserId && x.IsDefault);

                return new Response()
                {
                    Dashboard = dashboard.ToDto()
                };
            }
        }
    }
}
