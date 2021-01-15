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

namespace DblDip.Domain.Features.Profiles
{
    public class GetProfilesByCurrentAccount
    {
        public record Request : IRequest<Response>;

        public record Response(List<ProfileDto> Profiles);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IDblDipDbContext context, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var accountId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst(Constants.ClaimTypes.AccountId).Value);

                var profiles = await (from account in _context.Accounts
                                      
                                      join profile in _context.Profiles on account.AccountId equals profile.AccountId
                                      
                                      where account.AccountId == accountId
                                      
                                      select profile.ToDto()).ToListAsync();

                return new(profiles);
            }
        }
    }
}
