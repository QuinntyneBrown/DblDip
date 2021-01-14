using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
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
            public ReceiptDto Receipt { get; init; }
        }

        public class Response
        {
            public ReceiptDto Receipt { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var receipt = await _store.FindAsync<Receipt>(request.Receipt.ReceiptId);

                receipt.Update();

                _store.Add(receipt);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Receipt = receipt.ToDto()
                };
            }
        }
    }
}
