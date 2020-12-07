using BuildingBlocks.Core;
using ShootQ.Core.Exceptions;
using ShootQ.Domain.Features.Identity;
using ShootQ.Testing;
using ShootQ.Testing.Builders;
using System.Threading.Tasks;
using Xunit;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class IdentityControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;

        public IdentityControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_GetToken()
        {
            try
            {
                _fixture.Context.Store(UserBuilder.WithDefaults(_fixture.DataIntegrityService));

                await _fixture.Context.SaveChangesAsync(default);
            }
            catch (DomainException ex)
            {

            }

            var client = _fixture.CreateClient();

            var response = await client.PostAsAsync<dynamic, Authenticate.Response>("api/identity/token", new
            {
                username = "quinntynebrown@gmail.com",
                password = "shootq"
            });

            Assert.NotNull(response.AccessToken);

        }
    }
}
