using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateExpense
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Expense).NotNull();
                RuleFor(request => request.Expense).SetValidator(new ExpenseValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public ExpenseDto Expense { get; init; }
        }

        public class Response
        {
            public ExpenseDto Expense { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var expense = await _context.FindAsync<Expense>(request.Expense.ExpenseId);

                expense.Update();

                _context.Add(expense);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Expense = expense.ToDto()
                };
            }
        }
    }
}
