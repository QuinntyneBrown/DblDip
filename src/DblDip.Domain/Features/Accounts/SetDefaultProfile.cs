using BuildingBlocks.Core;
using DblDip.Core;
using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class SetDefaultProfile
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.ProfileId).NotEqual(Guid.NewGuid());
            }
        }

        public record Request(Guid ProfileId) : IRequest<ResponseBase>;
        public class Handler : IRequestHandler<Request, ResponseBase>
        {
            private readonly IDblDipDbContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IDblDipDbContext context, IHttpContextAccessor httpContextAccessor) {            
                _context = context;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseBase> Handle(Request request, CancellationToken cancellationToken) {

                var user = await _context.FindAsync<User>(new Guid(_httpContextAccessor.HttpContext.User.FindFirst(Constants.ClaimTypes.UserId).Value));

                var profile = await _context.FindAsync<Profile>(request.ProfileId);

                var account = await _context.FindAsync<Account>(profile.AccountId);

                if (account.UserId != user.UserId)
                    throw new Exception("Security Exception");

                account.SetDefaultProfileId(request.ProfileId);

                _context.Add(account);

                await _context.SaveChangesAsync(cancellationToken);

                return new ();
            }
        }
    }
}
