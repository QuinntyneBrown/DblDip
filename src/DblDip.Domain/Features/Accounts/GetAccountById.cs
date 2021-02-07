using BuildingBlocks.Core;
using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetAccountById
    {
        public class Request : IRequest<Response>
        {
            public Guid AccountId { get; set; }
        }

        public class Response: ResponseBase
        {
            public AccountDto Account { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var account = await _context.FindAsync<Account>(request.AccountId);

                return new Response()
                {
                    Account = account.ToDto()
                };
            }
        }
    }
}
