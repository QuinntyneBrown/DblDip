using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Invoices
{
    public class GetInvoiceById
    {
        public class Request : IRequest<Response>
        {
            public Guid InvoiceId { get; set; }
        }

        public class Response
        {
            public InvoiceDto Invoice { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var invoice = await _context.FindAsync<Invoice>(request.InvoiceId);

                return new Response()
                {
                    Invoice = invoice.ToDto()
                };
            }
        }
    }
}
