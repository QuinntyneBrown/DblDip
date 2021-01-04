using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetContactById
    {
        public class Request : IRequest<Response>
        {
            public Guid ContactId { get; init; }
        }

        public class Response
        {
            public ContactDto Contact { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var contact = await _context.FindAsync<Contact>(request.ContactId);

                return new Response()
                {
                    Contact = contact.ToDto()
                };
            }
        }
    }
}
