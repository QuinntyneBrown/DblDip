using BuildingBlocks.Abstractions;
using DblDip.Core;
using DblDip.Core.Models;
using DblDip.Domain.IntegrationEvents;
using MediatR;
using System;
using System.Threading;

namespace DblDip.Domain.Sagas
{
    public class ProfileCreatedSaga : INotificationHandler<ProfileCreated>
    {
        private readonly IAppDbContext _context;

        public ProfileCreatedSaga(IAppDbContext context)
            => _context = context;

        public async System.Threading.Tasks.Task Handle(ProfileCreated notification, CancellationToken cancellationToken)
        {
            var profile = notification.Profile;

            var user = new User(profile.Email, "default");

            var role = profile switch
            {
                Client => await _context.FindAsync<Role>(Constants.Roles.Client),
                Photographer => await _context.FindAsync<Role>(Constants.Roles.Photographer),
                ProjectManager => await _context.FindAsync<Role>(Constants.Roles.ProjectManager),
                SystemAdministrator => await _context.FindAsync<Role>(Constants.Roles.SystemAdministrator),
                _ => throw new NotImplementedException()
            };

            user.AddRole(role.RoleId, role.Name);

            var account = new Account(profile.ProfileId, profile.Name, user.UserId);

            _context.Store(user);

            _context.Store(account);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
