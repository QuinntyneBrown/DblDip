using DblDip.Core.Data;
using BuildingBlocks.Core;
using DblDip.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class Refresh
    {
        public class Request : IRequest<Response>
        {
            public string AccessToken { get; init; }
            public string RefreshToken { get; init; }
        }

        public class Response
        {
            public string AccessToken { get; init; }
            public string RefreshToken { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;
            private readonly ITokenProvider _tokenProvider;
            public Handler(IDblDipDbContext context, ITokenProvider tokenProvider)
            {
                _context = context;
                _tokenProvider = tokenProvider;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var principal = _tokenProvider.GetPrincipalFromExpiredToken(request.AccessToken);
                var username = principal.Identity.Name;
                var user = await _context.Set<User>().FirstOrDefaultAsync(x => x.Username == username);
                var refreshToken = user.RefreshToken;

                if (refreshToken != request.RefreshToken)
                {
                    return null;
                }

                var accessToken = _tokenProvider.Get(username);

                user.AddRefreshToken(_tokenProvider.GenerateRefreshToken());

                _context.Add(user);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response
                {
                    AccessToken = accessToken,
                    RefreshToken = user.RefreshToken
                };
            }
        }
    }
}
