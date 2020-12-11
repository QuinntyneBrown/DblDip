using BuildingBlocks.Core;
using DblDip.Core.Exceptions;
using DblDip.Domain.Features.Identity;
using DblDip.Testing;
using DblDip.Testing.Builders;
using System.Threading.Tasks;
using Xunit;

namespace DblDip.Api.FunctionalTests.Controllers
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
                password = "DblDip"
            });

            Assert.NotNull(response.AccessToken);

        }
    }
}
