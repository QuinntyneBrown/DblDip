using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateProjectManager
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ProjectManager).NotNull();
                RuleFor(request => request.ProjectManager).SetValidator(new ProjectManagerValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public ProjectManagerDto ProjectManager { get; init; }
        }

        public class Response: ResponseBase
        {
            public ProjectManagerDto ProjectManager { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var projectManager = new ProjectManager(default, default);

                _store.Add(projectManager);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    ProjectManager = projectManager.ToDto()
                };
            }
        }
    }
}
