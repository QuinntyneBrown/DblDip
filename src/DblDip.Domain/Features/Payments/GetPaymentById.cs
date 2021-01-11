using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
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
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

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
