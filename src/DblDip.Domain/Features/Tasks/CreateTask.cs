using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateTask
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Task).NotNull();
                RuleFor(request => request.Task).SetValidator(new TaskValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public TaskDto Task { get; init; }
        }

        public class Response
        {
            public TaskDto Task { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var task = new DblDip.Core.Models.Task(request.Task.OwnerId, request.Task.Description);

                _store.Add(task);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Task = task.ToDto()
                };
            }
        }
    }
}
