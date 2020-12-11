using DblDip.Core.Models;
using DblDip.Domain.IntegrationEvents;
using DblDip.Domain.Sagas;
using DblDip.Testing;
using DblDip.Testing.Builders.Core.Models;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DblDip.Domain.UnitTests.Sagas
{
    public class ProfileCreatedSagaTests
    {
        [Fact]
        public async System.Threading.Tasks.Task Should_CreateUserAndAccountWithRolesUponProfileCreated()
        {
            var context = AppDbContextBuilder.WithDefaults();

            var profile = ClientBuilder.WithDefaults();
            
            var sut = new ProfileCreatedSaga(context);

            var result = sut.Handle(new ProfileCreated(profile), default);

            var user = context.Set<User>().Where(x => x.Username == profile.Email);

            Assert.NotNull(user);
        }
    }
}
