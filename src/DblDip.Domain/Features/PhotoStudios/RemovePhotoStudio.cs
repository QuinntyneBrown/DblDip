using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features.PhotoStudios
{
    public class RemovePhotoStudio
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid PhotoStudioId { get; init; }
        }

        public class Response
        {
            public PhotoStudioDto PhotoStudio { get; init; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var photoStudio = await _context.FindAsync<PhotoStudio>(request.PhotoStudioId);

                //photoStudio.Remove();

                _context.Store(photoStudio);

                await _context.SaveChangesAsync(cancellationToken);

                return new()
                {

                };
            }
        }
    }
}
