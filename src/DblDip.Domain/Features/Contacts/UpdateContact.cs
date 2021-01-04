using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateContact
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Contact).NotNull();
                RuleFor(request => request.Contact).SetValidator(new ContactValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public ContactDto Contact { get; init; }
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

                var contact = await _context.FindAsync<Contact>(request.Contact.ContactId);

                contact.Update();

                _context.Store(contact);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Contact = contact.ToDto()
                };
            }
        }
    }
}
