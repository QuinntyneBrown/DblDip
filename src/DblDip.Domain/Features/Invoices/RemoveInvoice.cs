using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features.Invoices
{
    public class RemoveInvoice
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid InvoiceId { get; init; }
        }

        public class Response
        {
            public InvoiceDto Invoice { get; init; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var invoice = await _context.FindAsync<Invoice>(request.InvoiceId);

                //invoice.Remove();

                _context.Store(invoice);

                await _context.SaveChangesAsync(cancellationToken);

                return new ()
                {

                };
            }
        }
    }
}
