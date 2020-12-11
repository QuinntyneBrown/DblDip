using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ShootQ.Domain.Features.SystemAdministrators
{
    public class RemoveSystemAdministrator
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit> {  
            public Guid SystemAdministratorId { get; set; }
        }

        public class Response
        {
            public SystemAdministratorDto SystemAdministrator { get; set; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken) {

                var systemAdministrator = await _context.FindAsync<SystemAdministrator>(request.SystemAdministratorId);

                //systemAdministrator.Remove();

                _context.Store(systemAdministrator);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit()
                {

                };
            }
        }
    }
}
