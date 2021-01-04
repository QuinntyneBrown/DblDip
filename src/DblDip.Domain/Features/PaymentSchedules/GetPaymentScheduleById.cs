using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetPaymentScheduleById
    {
        public class Request : IRequest<Response> {  
            public Guid PaymentScheduleId { get; set; }        
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

                var paymentSchedule = await _context.FindAsync<PaymentSchedule>(request.PaymentScheduleId);

                return new Response() { 
                    PaymentSchedule = paymentSchedule.ToDto()
                };
            }
        }
    }
}
