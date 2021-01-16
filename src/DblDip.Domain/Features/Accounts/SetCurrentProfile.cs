using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class SetCurrentProfile
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.ProfileId).NotEqual(Guid.NewGuid());
            }
        }
        public record Request(Guid ProfileId): IRequest<Response>;
    
        public record Response(string AccessToken);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;
            private readonly ITokenBuilder _tokenBuilder;
            private readonly IHttpContextAccessor _httpContextAccessor;
            public Handler(IEventStore store, ITokenBuilder tokenBuilder, IHttpContextAccessor httpContextAccessor)
            {
                _store = store;
                _tokenBuilder = tokenBuilder;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var claimsPrincipal = _httpContextAccessor.HttpContext.User as ClaimsPrincipal;

                var accountId = new Guid(claimsPrincipal.FindFirst(Constants.ClaimTypes.AccountId).Value);

                var account = await _store.FindAsync<Account>(accountId);

                account.SetCurrentProfileId(request.ProfileId);

                await _store.SaveChangesAsync(default);

                return new (_tokenBuilder
                    .FromClaimsPrincipal(claimsPrincipal)
                    .AddOrUpdateClaim(new Claim(Constants.ClaimTypes.ProfileId, $"{request.ProfileId}"))
                    .Build());
            }
        }
    }
}
