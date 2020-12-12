using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Tickets
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

        public class Request : IRequest<Response> {  
            public TicketDto Ticket { get; set; }
        }

        public class Response
        {
            public TicketDto Ticket { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var ticket = new Ticket();

                _context.Store(ticket);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Ticket = ticket.ToDto()
                };
            }
        }
    }
}
