using DblDip.Core.Data;
using DblDip.Core;
using DblDip.Core.Models;
using DblDip.Domain.IntegrationEvents;
using MediatR;
using System;
using System.Threading;
using BuildingBlocks.EventStore;

namespace DblDip.Domain.Sagas
{
    public class ProfileCreatedSaga : INotificationHandler<ProfileCreated>
    {
        private readonly IEventStore _store;

        public ProfileCreatedSaga(IEventStore store)
            => _store = store;

        public async System.Threading.Tasks.Task Handle(ProfileCreated notification, CancellationToken cancellationToken)
        {
            var profile = notification.Profile;

            var user = new User(profile.Email, "default");

            var role = profile switch
            {
                Client => await _store.FindAsync<Role>(Constants.Roles.Client),
                Photographer => await _store.FindAsync<Role>(Constants.Roles.Photographer),
                ProjectManager => await _store.FindAsync<Role>(Constants.Roles.ProjectManager),
                SystemAdministrator => await _store.FindAsync<Role>(Constants.Roles.SystemAdministrator),
                _ => throw new NotImplementedException()
            };

            user.AddRole(role.RoleId, role.Name);

            var account = new Account(profile.ProfileId, profile.Name, user.UserId);

            _store.Add(user);

            _store.Add(account);

            await _store.SaveChangesAsync(cancellationToken);
        }
    }
}
