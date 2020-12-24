using FluentValidation;
using MediatR;
using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using DblDip.Core.ValueObjects;
using System;
using BuildingBlocks.Core;
using System.Collections.Generic;
using System.Security.Claims;

namespace DblDip.Domain.Features.Identity
{
    public class AuthenticateByEmailAndQuoteId
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Email).NotNull();
                RuleFor(x => x.QuoteId).NotNull();
            }
        }

        public record Request(string Email, Guid QuoteId) : IRequest<Response>;

        public record Response(string AccessToken);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly ITokenProvider _tokenProvider;

            public Handler(IAppDbContext context, ITokenProvider tokenProvider)
            {
                _context = context;
                _tokenProvider = tokenProvider;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var user = _context.Set<User>().Where(x => x.Username == request.Email).SingleOrDefault();

                if (user != null)
                    return null;

                var quote = await _context.FindAsync<Quote>(request.QuoteId);

                if (quote == null || quote.Declined.HasValue || quote.Accepted.HasValue)
                    return null;

                return new Response(_tokenProvider.Get(request.Email, new List<Claim> {

                    new Claim(Core.Constants.ClaimTypes.Role,nameof(Core.Constants.Roles.Lead))
                }));
            }
        }
    }
}
