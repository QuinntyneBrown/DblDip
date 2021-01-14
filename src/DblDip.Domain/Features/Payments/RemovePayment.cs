using BuildingBlocks.EventStore;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features
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
            private readonly IEventStore _store;
            private readonly IDateTime _dateTime;

            public Handler(IEventStore store, IDateTime dateTime) => (_store, _dateTime) = (store, dateTime);

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var payment = await _store.FindAsync<Payment>(request.PaymentId);

                payment.Remove(_dateTime.UtcNow);

                _store.Add(payment);

                await _store.SaveChangesAsync(cancellationToken);

                return new();
            }
        }
    }
}
