using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetExpenseById
    {
        public class Request : IRequest<Response>
        {
            public Guid ExpenseId { get; init; }
        }

        public class Response
        {
            public ExpenseDto Expense { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var expense = await _context.FindAsync<Expense>(request.ExpenseId);

                return new Response()
                {
                    Expense = expense.ToDto()
                };
            }
        }
    }
}
