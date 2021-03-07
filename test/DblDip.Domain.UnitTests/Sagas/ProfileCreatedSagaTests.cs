using DblDip.Core.Models;
using DblDip.Domain.Sagas;
using DblDip.Testing;
using DblDip.Testing.Builders;
using System.Linq;
using Xunit;
using static DblDip.Core.Constants;

namespace DblDip.Domain.UnitTests.Sagas
{
    public class ProfileCreatedSagaTests
    {
        [Fact]
        public async System.Threading.Tasks.Task Should_CreateUserAndAccountWithRolesUponProfileCreated()
        {
            var context = DblDipDbContextBuilder.WithDefaults();
            
            var store = EventStoreBuilder.WithDefaults();
            
            var profile = ClientBuilder.WithDefaults();

            var sut = new ProfileCreatedSaga(store);

            await sut.Handle(new (profile), default);

            var user = context.Set<User>().Where(x => x.Username == profile.Email).Single();

            Assert.NotNull(user);

            Assert.Contains(user.Roles, x => x.RoleId == Roles.Client);
        }
    }
}
