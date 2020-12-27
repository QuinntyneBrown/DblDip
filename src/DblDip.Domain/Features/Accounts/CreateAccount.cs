using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Accounts
{
    public class CreateAccount
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Account).NotNull();
                RuleFor(request => request.Account).SetValidator(new AccountValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public AccountDto Account { get; set; }
        }

        public class Response
        {
            public AccountDto Account { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var account = new Account(default,request.Account.DefaultProfileId,request.Account.Name, request.Account.AccountHolderUserId);

                _context.Store(account);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Account = account.ToDto()
                };
            }
        }
    }
}
