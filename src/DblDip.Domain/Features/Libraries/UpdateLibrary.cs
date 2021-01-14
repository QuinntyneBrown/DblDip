using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateLibrary
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Library).NotNull();
                RuleFor(request => request.Library).SetValidator(new LibraryValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public LibraryDto Library { get; init; }
        }

        public class Response
        {
            public LibraryDto Library { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var library = await _store.FindAsync<Library>(request.Library.LibraryId);

                library.Update();

                _store.Add(library);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Library = library.ToDto()
                };
            }
        }
    }
}
