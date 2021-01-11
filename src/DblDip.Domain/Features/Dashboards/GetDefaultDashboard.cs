using DblDip.Core.Data;
using DblDip.Core;
using DblDip.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
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
            private readonly IDblDipDbContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IDblDipDbContext context, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var profileId = new Guid(_httpContextAccessor.HttpContext.User
                    .FindFirst(Constants.ClaimTypes.ProfileId).Value);

                var profile = await _context.FindAsync<Profile>(profileId);

                var dashboard = await _context.FindAsync<Dashboard>(profile.DefaultDashboardId.Value);

                return new Response()
                {
                    Dashboard = dashboard.ToDto()
                };
            }
        }
    }
}
