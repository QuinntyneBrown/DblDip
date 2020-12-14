using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.ShotLists
{
    public class GetShotListById
    {
        public class Request : IRequest<Response> {  
            public Guid ShotListId { get; set; }        
        }

        public class Response
        {
            public ShotListDto ShotList { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var shotList = await _context.FindAsync<ShotList>(request.ShotListId);

                return new Response() { 
                    ShotList = shotList.ToDto()
                };
            }
        }
    }
}
