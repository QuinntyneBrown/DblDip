using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetPaymentSchedules
    {
        public class Request : IRequest<Response> {  }

        public class Response
        {
            public List<PaymentScheduleDto> PaymentSchedules { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
			    return new Response() { 
                    PaymentSchedules = _context.Set<PaymentSchedule>().Select(x => x.ToDto()).ToList()
                };
            }
        }
    }
}
