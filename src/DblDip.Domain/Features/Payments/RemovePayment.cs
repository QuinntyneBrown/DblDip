using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features.Payments
{
    public class RemovePayment
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid PaymentId { get; set; }
        }

        public class Response
        {
            public PaymentDto Payment { get; set; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IDateTime _dateTime;

            public Handler(IAppDbContext context, IDateTime dateTime) => (_context, _dateTime) = (context, dateTime);

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var payment = await _context.FindAsync<Payment>(request.PaymentId);

                payment.Remove(_dateTime.UtcNow);

                _context.Store(payment);

                await _context.SaveChangesAsync(cancellationToken);

                return new();
            }
        }
    }
}
