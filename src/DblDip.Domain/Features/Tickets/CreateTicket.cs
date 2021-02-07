using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateTicket
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Ticket).NotNull();
                RuleFor(request => request.Ticket).SetValidator(new TicketValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public TicketDto Ticket { get; init; }
        }

        public class Response: ResponseBase
        {
            public TicketDto Ticket { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var ticket = new Ticket();

                _store.Add(ticket);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Ticket = ticket.ToDto()
                };
            }
        }
    }
}
