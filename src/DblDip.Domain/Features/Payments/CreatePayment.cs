using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreatePayment
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Payment).NotNull();
                RuleFor(request => request.Payment).SetValidator(new PaymentValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public PaymentDto Payment { get; set; }
        }

        public class Response
        {
            public PaymentDto Payment { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var payment = new Payment();

                _store.Add(payment);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Payment = payment.ToDto()
                };
            }
        }
    }
}
