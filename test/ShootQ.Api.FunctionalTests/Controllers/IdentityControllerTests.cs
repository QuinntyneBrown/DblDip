using BuildingBlocks.Core;
using ShootQ.Domain.Features.Identity;
using ShootQ.Testing;
using ShootQ.Testing.Builders;
using System.Threading.Tasks;
using Xunit;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class IdentityControllerTests: IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;

        public IdentityControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_GetToken()
        {
            _fixture.Context.Store(UserBuilder.WithDefaults());

            await _fixture.Context.SaveChangesAsync(default);

            var client = _fixture.CreateClient();

            var response = await client.PostAsAsync<dynamic, Authenticate.Response>("api/identity/token", new
            {
                username = "quinntynebrown@gmail.com",
                password = "ShootQ"
            });

            Assert.NotNull(response.AccessToken);

        }
    }
}
