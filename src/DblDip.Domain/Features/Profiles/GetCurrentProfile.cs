using BuildingBlocks.Abstractions;
using DblDip.Core;
using DblDip.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Profiles
{
    public class GetCurrentProfile
    {
        public record Request:IRequest<Response>;

        public record Response(ProfileDto Profile);

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

                var profileId = new Guid(_httpContextAccessor.HttpContext.User
                    .FindFirst(Constants.ClaimTypes.ProfileId).Value);

                var profile = await _context.FindAsync<Profile>(profileId);

                return new(profile.ToDto());
            }
        }
    }
}
