using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.PhotoStudios
{
    public class UpdatePhotoStudio
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.PhotoStudio).NotNull();
                RuleFor(request => request.PhotoStudio).SetValidator(new PhotoStudioValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public PhotoStudioDto PhotoStudio { get; set; }
        }

        public class Response
        {
            public PhotoStudioDto PhotoStudio { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var photoStudio = await _context.FindAsync<PhotoStudio>(request.PhotoStudio.PhotoStudioId);

                //photoStudio.Update();

                _context.Store(photoStudio);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    PhotoStudio = photoStudio.ToDto()
                };
            }
        }
    }
}
