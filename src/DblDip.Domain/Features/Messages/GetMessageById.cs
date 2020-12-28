using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Messages
{
    public class GetMessageById
    {
        public class Request : IRequest<Response> {  
            public Guid MessageId { get; set; }        
        }

        public class Response
        {
            public MessageDto Message { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var message = await _context.FindAsync<Message>(request.MessageId);

                return new Response() { 
                    Message = message.ToDto()
                };
            }
        }
    }
}
