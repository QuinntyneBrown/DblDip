using DblDip.Core;
using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
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

                var profileId = new Guid(_httpContextAccessor.HttpContext.User
                    .FindFirst(Constants.ClaimTypes.ProfileId).Value);

                var dashboards = _context.Set<Dashboard>().Where(x => x.ProfileId == profileId);

                return new (_context.Set<Dashboard>().Select(x => x.ToDto()).ToList());
            }
        }
    }
}
