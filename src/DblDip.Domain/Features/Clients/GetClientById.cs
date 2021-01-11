using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetClientById
    {
        public record Request(Guid ClientId) : IRequest<Response>;

        public record Response(ClientDto Client);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var client = await _context.FindAsync<Client>(request.ClientId);

                return new(client.ToDto());
            }
        }
    }
}
