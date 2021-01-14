using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class Refresh
    {
        public record Request(string AccessToken, string RefreshToken) : IRequest<Response>;

        public record Response(string AccessToken, string RefreshToken);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;
            private readonly IEventStore _store;
            private readonly ITokenProvider _tokenProvider;
            public Handler(IDblDipDbContext context, IEventStore store, ITokenProvider tokenProvider)
            {
                _context = context;
                _tokenProvider = tokenProvider;
                _store = store;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var principal = _tokenProvider.GetPrincipalFromExpiredToken(request.AccessToken);
                var username = principal.Identity.Name;
                var user = await _context.Users.SingleAsync(x => x.Username == username, cancellationToken);
                var refreshToken = user.RefreshToken;

                if (refreshToken != request.RefreshToken)
                {
                    return null;
                }

                var accessToken = _tokenProvider.Get(username);

                user.AddRefreshToken(_tokenProvider.GenerateRefreshToken());

                _store.Add(user);

                await _store.SaveChangesAsync(cancellationToken);

                return new (accessToken, user.RefreshToken);
            }
        }
    }
}
