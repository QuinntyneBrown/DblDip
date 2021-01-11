using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetPhotoStudioById
    {
        public class Request : IRequest<Response>
        {
            public Guid PhotoStudioId { get; init; }
        }

        public class Response
        {
            public PhotoStudioDto PhotoStudio { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

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
