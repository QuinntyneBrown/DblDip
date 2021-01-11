using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetTicketById
    {
        public class Request : IRequest<Response>
        {
            public Guid TicketId { get; init; }
        }

        public class Response
        {
            public TicketDto Ticket { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var ticket = await _context.FindAsync<Ticket>(request.TicketId);

                return new Response()
                {
                    Ticket = ticket.ToDto()
                };
            }
        }
    }
}
