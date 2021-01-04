using BuildingBlocks.Abstractions;
using DblDip.Core;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetCurrentAccountProfiles
    {
        public record Request : IRequest<Response>;

        public record Response(List<ProfileDto> Profiles);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IAppDbContext context, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var user = await _context.FindAsync<User>(new Guid(_httpContextAccessor.HttpContext.User.FindFirst(Constants.ClaimTypes.UserId).Value));

                var account = _context.Set<Account>().Single(x => x.UserId == user.UserId);

                return new Response(_context.Set<Profile>(account.ProfileIds.ToList())
                    .Select(x => x.ToDto())
                    .ToList());
            }
        }
    }
}
