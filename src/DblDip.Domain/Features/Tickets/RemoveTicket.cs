using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features.Tickets
{
    public class RemoveTicket
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid TicketId { get; init; }
        }

        public class Response
        {
            public TicketDto Ticket { get; init; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IDateTime _dateTime;

            public Handler(IAppDbContext context, IDateTime dateTime)
            {
                _context = context;
                _dateTime = dateTime;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var ticket = await _context.FindAsync<Ticket>(request.TicketId);

                ticket.Remove(_dateTime.UtcNow);

                _context.Store(ticket);

                await _context.SaveChangesAsync(cancellationToken);

                return new();
            }
        }
    }
}
