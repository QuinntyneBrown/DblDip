using BuildingBlocks.Core;
using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetConversationById
    {
        public class Request : IRequest<Response>
        {
            public Guid ConversationId { get; set; }
        }

        public class Response: ResponseBase
        {
            public ConversationDto Conversation { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var conversation = await _context.FindAsync<Conversation>(request.ConversationId);

                return new Response()
                {
                    Conversation = conversation.ToDto()
                };
            }
        }
    }
}
