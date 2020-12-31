using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using DblDip.Domain.Features.Dashboards;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Profiles
{
    public class GetDefaultDashboard
    {
        public record Request: IRequest<Response>;

        public record Response(DashboardDto Dashboard);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IAppDbContext context){
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                return new(default);
            }
        }
    }
}
