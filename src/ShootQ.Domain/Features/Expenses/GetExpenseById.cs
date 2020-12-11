using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Expenses
{
    public class GetExpenseById
    {
        public class Request : IRequest<Response> {  
            public Guid ExpenseId { get; set; }        
        }

        public class Response
        {
            public ExpenseDto Expense { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var expense = await _context.FindAsync<Expense>(request.ExpenseId);

                return new Response() { 
                    Expense = expense.ToDto()
                };
            }
        }
    }
}
