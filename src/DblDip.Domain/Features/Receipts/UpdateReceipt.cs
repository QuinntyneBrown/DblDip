using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Receipts
{
    public class UpdateReceipt
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Receipt).NotNull();
                RuleFor(request => request.Receipt).SetValidator(new ReceiptValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public ReceiptDto Receipt { get; set; }
        }

        public class Response
        {
            public ReceiptDto Receipt { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var receipt = await _context.FindAsync<Receipt>(request.Receipt.ReceiptId);

                //receipt.Update();

                _context.Store(receipt);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Receipt = receipt.ToDto()
                };
            }
        }
    }
}
