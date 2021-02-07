using BuildingBlocks.Core;
using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetInvoiceById
    {
        public class Request : IRequest<Response>
        {
            public Guid InvoiceId { get; init; }
        }

        public class Response: ResponseBase
        {
            public InvoiceDto Invoice { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

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
