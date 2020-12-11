using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Libraries
{
    public class CreateLibrary
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Library).NotNull();
                RuleFor(request => request.Library).SetValidator(new LibraryValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public LibraryDto Library { get; set; }
        }

        public class Response
        {
            public LibraryDto Library { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var library = new Library();

                _context.Store(library);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Library = library.ToDto()
                };
            }
        }
    }
}
