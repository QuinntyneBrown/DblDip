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
        private readonly IDblDipDbContext _context;

        public QuoteCreatedSaga(IDblDipDbContext context)
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

                var account = new Account(profile.ProfileId, "", user.UserId);

                _context.Add(profile);

                _context.Add(account);

                _context.Add(user);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
