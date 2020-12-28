using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Payments
{
    public class GetPaymentById
    {
        public class Request : IRequest<Response>
        {
            public Guid PaymentId { get; set; }
        }

        public class Response
        {
            public PaymentDto Payment { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var payment = await _context.FindAsync<Payment>(request.PaymentId);

                return new Response()
                {
                    Payment = payment.ToDto()
                };
            }
        }
    }
}
