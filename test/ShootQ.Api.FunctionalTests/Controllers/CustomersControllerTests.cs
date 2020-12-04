using ShootQ.Domain.Features.Customers;
using ShootQ.Testing;
using System.Threading.Tasks;
using Xunit;
using BuildingBlocks.Core;
using ShootQ.Testing.Builders.Domain.Dtos;

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

        [Fact]
        public async Task Should_AddCustomer()
        {
            var client = _fixture.CreateAuthenticatedClient();
             
            var customer = new CustomerDtoBuilder().Build();

            var response = await client.PostAsAsync<dynamic,CreateCustomer.Response>("api/customers",new { customer });

            Assert.NotEqual(default, response.Customer.CustomerId);

        }
    }
}
