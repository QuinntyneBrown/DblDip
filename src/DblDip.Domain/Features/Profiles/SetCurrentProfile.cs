using FluentValidation;
using MediatR;
using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using BuildingBlocks.Core;
using DblDip.Core;
using System.Collections.Generic;
using System.Security.Claims;

namespace DblDip.Domain.Features.Profiles
{
    public class SetCurrentProfile
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public record Request(Guid ProfileId): IRequest<Response>;
    
        public record Response(string AccessToken);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly ITokenProvider _tokenProvider;
            private readonly IHttpContextAccessor _httpContextAccessor;
            public Handler(IAppDbContext context, ITokenProvider tokenProvider, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _tokenProvider = tokenProvider;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var user = await _context.FindAsync<User>(new Guid(_httpContextAccessor.HttpContext.User.FindFirst(Constants.ClaimTypes.UserId).Value));

                var profile = await _context.FindAsync<Profile>(request.ProfileId);

                var account = await _context.FindAsync<Account>(profile.AccountId);

                if (account.UserId != user.UserId)
                    throw new Exception("Security Exception");

                return new Response(_tokenProvider.Get(user.Username, new List<Claim> {
                    new (Constants.ClaimTypes.UserId, $"{user.UserId}"),
                    new (Constants.ClaimTypes.ProfileId, $"{request.ProfileId}")
                }));
            }
        }
    }
}
