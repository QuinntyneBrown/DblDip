using DblDip.Core.Data;
using BuildingBlocks.Core;
using FluentValidation;
using MediatR;
using DblDip.Core;
using DblDip.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DblDip.Domain.Extensions;

namespace DblDip.Domain.Features
{
    public class Authenticate
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Username).NotNull();
                RuleFor(x => x.Password).NotNull();
            }
        }

        public record Request(string Username, string Password) : IRequest<Response>;

        public record Response(string AccessToken, Guid UserId);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;
            private readonly IPasswordHasher _passwordHasher;
            private readonly ITokenProvider _tokenProvider;
            private readonly ITokenBuilder _tokenBuilder;

            public Handler(IDblDipDbContext context, ITokenProvider tokenProvider, IPasswordHasher passwordHasher, ITokenBuilder tokenBuilder)
            {
                _context = context;
                _tokenProvider = tokenProvider;
                _passwordHasher = passwordHasher;
                _tokenBuilder = tokenBuilder;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var userAccountRoles = await(from u in _context.Users
                                             join a in _context.Accounts on u.UserId equals a.UserId
                                             join rr in _context.Users.SelectMany(x => x.Roles) on true equals true 
                                             join r in _context.Roles on rr.RoleId equals r.RoleId
                                             where u.Username == request.Username
                                             select new
                                             {
                                                 User = u,
                                                 Role = r,
                                                 Account = a
                                             }).ToListAsync();

                var user = userAccountRoles.FirstOrDefault().User;

                var account = userAccountRoles.First().Account;

                if (user == null)
                    throw new Exception();

                if (!ValidateUser(user, _passwordHasher.HashPassword(user.Salt, request.Password)))
                    throw new Exception();


                _tokenBuilder.AddUsername(user.Username);

                foreach (var role in userAccountRoles.Select(x => x.Role))
                {
                    _tokenBuilder.AddOrUpdateClaim(new Claim(Constants.ClaimTypes.Role, role.Name));
                }

                _tokenBuilder.AddOrUpdateClaim(new Claim(Constants.ClaimTypes.AccountId, $"{account.AccountId}"));

                _tokenBuilder.AddOrUpdateClaim(new Claim(Constants.ClaimTypes.UserId, $"{account.UserId}"));

                return new(_tokenBuilder.Build(), user.UserId);

            }

            public bool ValidateUser(User user, string transformedPassword)
            {
                if (user == null || transformedPassword == null)
                    return false;

                return user.Password == transformedPassword;
            }
        }
    }
}
