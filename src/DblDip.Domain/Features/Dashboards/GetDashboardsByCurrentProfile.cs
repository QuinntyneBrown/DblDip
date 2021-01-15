using DblDip.Core;
using DblDip.Core.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetDashboardsByCurrentProfile
    {
        public record Request : IRequest<Response>;

        public record Response(List<DashboardDto> Dashboards);

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

                var profileId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst(Constants.ClaimTypes.ProfileId).Value);

                var dashboards = await _context.Dashboards.Where(x => x.ProfileId == profileId).Select(x => x.ToDto()).ToListAsync();

                return new (dashboards);
            }
        }
    }
}
