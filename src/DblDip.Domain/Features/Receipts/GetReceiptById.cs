using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Receipts
{
    public class GetReceiptById
    {
        public class Request : IRequest<Response>
        {
            public Guid ReceiptId { get; init; }
        }

        public class Response
        {
            public ReceiptDto Receipt { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var receipt = await _context.FindAsync<Receipt>(request.ReceiptId);

                return new Response()
                {
                    Receipt = receipt.ToDto()
                };
            }
        }
    }
}
