using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Tickets
{
    public class GetTicketById
    {
        public class Request : IRequest<Response> {  
            public Guid TicketId { get; set; }        
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

                var ticket = await _context.FindAsync<Ticket>(request.TicketId);

                return new Response() { 
                    Ticket = ticket.ToDto()
                };
            }
        }
    }
}
