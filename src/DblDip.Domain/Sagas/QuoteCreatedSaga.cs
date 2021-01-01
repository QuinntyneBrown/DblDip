using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using DblDip.Domain.IntegrationEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DblDip.Domain.Sagas
{
    public class QuoteCreatedSaga : INotificationHandler<QuoteCreated>
    {
        private readonly IAppDbContext _context;

        public QuoteCreatedSaga(IAppDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task Handle(QuoteCreated notification, CancellationToken cancellationToken)
        {
            var primaryParticpantEmail = notification.Quote.BillToEmail;

            var user = _context.Set<User>().SingleOrDefault(x => x.Username == primaryParticpantEmail);

            user ??= new User(primaryParticpantEmail);

            if (user.DomainEvents.Any())
            {
                var profile = new Lead(primaryParticpantEmail);

                var account = new Account(new List<Guid> { profile.ProfileId }, profile.ProfileId, "", user.UserId);

                _context.Store(profile);

                _context.Store(account);

                _context.Store(user);

                await _context.SaveChangesAsync(default);
            }

        }
    }
}
