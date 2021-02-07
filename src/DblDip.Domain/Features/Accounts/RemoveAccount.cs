using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using BuildingBlocks.Core;

namespace DblDip.Domain.Features
{
    public class RemoveAccount
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<ResponseBase>
        {
            public Guid AccountId { get; set; }
        }

        public class Response
        {
            public AccountDto Account { get; set; }
        }

        public class Handler : IRequestHandler<Request, ResponseBase>
        {
            private readonly IEventStore _store;
            private readonly IDateTime _dateTime;

            public Handler(IEventStore store, IDateTime dateTime)
            {
                _store = store;
                _dateTime = dateTime;
            }

            public async Task<ResponseBase> Handle(Request request, CancellationToken cancellationToken)
            {

                var account = await _store.FindAsync<Account>(request.AccountId);

                account.Remove(_dateTime.UtcNow);

                _store.Add(account);

                await _store.SaveChangesAsync(cancellationToken);

                return new();
            }
        }
    }
}
