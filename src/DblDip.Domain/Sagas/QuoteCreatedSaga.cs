using BuildingBlocks.EventStore;
using DblDip.Core.Data;
using DblDip.Core.Models;
using DblDip.Domain.IntegrationEvents;
using MediatR;
using System.Linq;
using System.Threading;

namespace DblDip.Domain.Sagas
{
    public class QuoteCreatedSaga : INotificationHandler<QuoteCreated>
    {
        private readonly IEventStore _store;
        private readonly IDblDipDbContext _context;

        public QuoteCreatedSaga(IDblDipDbContext context, IEventStore store)
        {
            _context = context;
            _store = store;
        }

        public async System.Threading.Tasks.Task Handle(QuoteCreated notification, CancellationToken cancellationToken)
        {
            var primaryParticpantEmail = notification.Quote.BillToEmail;

            var user = _context.Set<User>().SingleOrDefault(x => x.Username == primaryParticpantEmail);

            user ??= new User(primaryParticpantEmail);

            if (user.DomainEvents.Any())
            {
                var profile = new Lead(primaryParticpantEmail);

                var account = new Account(profile.ProfileId, "", user.UserId);

                _store.Add(profile);

                _store.Add(account);

                _store.Add(user);

                await _store.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
