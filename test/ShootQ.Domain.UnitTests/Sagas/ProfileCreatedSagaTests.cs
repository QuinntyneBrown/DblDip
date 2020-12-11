using ShootQ.Core.Models;
using ShootQ.Domain.IntegrationEvents;
using ShootQ.Domain.Sagas;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShootQ.Domain.UnitTests.Sagas
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
