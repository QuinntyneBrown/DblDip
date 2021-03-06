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
    public class GetCardById
    {
        public class Request : IRequest<Response>
        {
            public Guid CardId { get; init; }
        }

        public class Response: ResponseBase
        {
            public CardDto Card { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var card = await _context.FindAsync<Card>(request.CardId);

                return new Response()
                {
                    Card = card.ToDto()
                };
            }
        }
    }
}
