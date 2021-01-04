using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateInvoice
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Invoice).NotNull();
                RuleFor(request => request.Invoice).SetValidator(new InvoiceValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public InvoiceDto Invoice { get; init; }
        }

        public class Response
        {
            public InvoiceDto Invoice { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var invoice = new Invoice();

                _context.Store(invoice);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Invoice = invoice.ToDto()
                };
            }
        }
    }
}
