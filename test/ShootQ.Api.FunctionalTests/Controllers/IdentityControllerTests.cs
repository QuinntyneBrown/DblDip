using ShootQ.Testing;
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
        public IdentityControllerTests()
        {

        }

        [Fact]
        public async Task Should()
        {

        }
    }
}
