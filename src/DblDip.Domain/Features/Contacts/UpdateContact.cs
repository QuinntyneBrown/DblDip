using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
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

        public class Response: ResponseBase
        {
            public ContactDto Contact { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var contact = await _store.FindAsync<Contact>(request.Contact.ContactId);

                contact.Update();

                _store.Add(contact);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Contact = contact.ToDto()
                };
            }
        }
    }
}
