using BuildingBlocks.Core;
using ShootQ.Core.Models;
using ShootQ.Domain.Features.Customers;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Domain.Dtos;
using Xunit;

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
        public async System.Threading.Tasks.Task Should_GetCustomers()
        {
            _ = _fixture.Context.Store(new Customer());

            await _fixture.Context.SaveChangesAsync(default);

            var response = await _fixture.CreateAuthenticatedClient().GetAsync<GetCustomers.Response>("api/customers");

            Assert.NotEmpty(response.Customers);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_AddCustomer()
        {
            var client = _fixture.CreateAuthenticatedClient();
             
            var customer = new CustomerDtoBuilder().Build();

            var response = await client.PostAsAsync<dynamic,CreateCustomer.Response>("api/customers",new { customer });

            Assert.NotEqual(default, response.Customer.CustomerId);

        }
    }
}
