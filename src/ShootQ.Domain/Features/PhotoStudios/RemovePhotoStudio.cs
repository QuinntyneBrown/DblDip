using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ShootQ.Domain.Features.PhotoStudios
{
    public class RemovePhotoStudio
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit> {  
            public Guid PhotoStudioId { get; set; }
        }

        public class Response
        {
            public PhotoStudioDto PhotoStudio { get; set; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken) {

                var photoStudio = await _context.FindAsync<PhotoStudio>(request.PhotoStudioId);

                //photoStudio.Remove();

                _context.Store(photoStudio);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit()
                {

                };
            }
        }
    }
}
