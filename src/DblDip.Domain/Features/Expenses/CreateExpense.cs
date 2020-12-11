using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Expenses
{
    public class CreateExpense
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Expense).NotNull();
                RuleFor(request => request.Expense).SetValidator(new ExpenseValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public ExpenseDto Expense { get; set; }
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

                var expense = new Expense();

                _context.Store(expense);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Expense = expense.ToDto()
                };
            }
        }
    }
}
