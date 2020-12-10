using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.PhotoStudios
{
    public class GetPhotoStudioById
    {
        public class Request : IRequest<Response>
        {
            public Guid PhotoStudioId { get; set; }
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

                var photoStudio = await _context.FindAsync<PhotoStudio>(request.PhotoStudioId);

                return new Response()
                {
                    PhotoStudio = photoStudio.ToDto()
                };
            }
        }
    }
}
