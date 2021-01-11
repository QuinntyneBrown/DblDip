using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdatePaymentSchedule
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.PaymentSchedule).NotNull();
                RuleFor(request => request.PaymentSchedule).SetValidator(new PaymentScheduleValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public PaymentScheduleDto PaymentSchedule { get; set; }
        }

        public class Response
        {
            public PaymentScheduleDto PaymentSchedule { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var paymentSchedule = await _context.FindAsync<PaymentSchedule>(request.PaymentSchedule.PaymentScheduleId);

                paymentSchedule.Update();

                _context.Add(paymentSchedule);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    PaymentSchedule = paymentSchedule.ToDto()
                };
            }
        }
    }
}
