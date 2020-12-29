using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.PaymentSchedules
{
    public class CreatePaymentSchedule
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
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var paymentSchedule = new PaymentSchedule();

                _context.Store(paymentSchedule);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    PaymentSchedule = paymentSchedule.ToDto()
                };
            }
        }
    }
}
