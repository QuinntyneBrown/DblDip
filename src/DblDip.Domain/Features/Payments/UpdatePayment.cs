using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Payments
{
    public class UpdatePayment
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Payment).NotNull();
                RuleFor(request => request.Payment).SetValidator(new PaymentValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public PaymentDto Payment { get; set; }
        }

        public class Response
        {
            public PaymentDto Payment { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var payment = await _context.FindAsync<Payment>(request.Payment.PaymentId);

                payment.Update();

                _context.Store(payment);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Payment = payment.ToDto()
                };
            }
        }
    }
}
