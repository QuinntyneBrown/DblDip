using ShootQ.Domain.Features.Customers;
using ShootQ.Testing;
using System.Threading.Tasks;
using Xunit;
using BuildingBlocks.Core;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class CustomersControllerTests: IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;

        public CustomersControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async Task Should_GetCustomers()
        {
            var response = await _fixture.CreateAuthenticatedClient().GetAsync<GetCustomers.Response>("api/customers");

            Assert.Empty(response.Customers);

        }
    }
}
