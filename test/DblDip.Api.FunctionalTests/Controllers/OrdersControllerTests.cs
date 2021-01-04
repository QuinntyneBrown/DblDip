using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.Orders;
using DblDip.Testing;
using DblDip.Testing.Builders;
using DblDip.Testing.Builders;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.OrdersControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class OrdersControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public OrdersControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateOrder()
        {
            var context = _fixture.Context;

            var order = OrderDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { order }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateOrder, stringContent);

            var response = JsonConvert.DeserializeObject<CreateOrder.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Order>(response.Order.OrderId);

            Assert.NotEqual(default, response.Order.OrderId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveOrder()
        {
            var order = OrderBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(order);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(order.OrderId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedOrder = await context.FindAsync<Order>(order.OrderId);

            Assert.NotEqual(default, removedOrder.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateOrder()
        {
            var order = OrderBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(order);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { order = order.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Order>(order.OrderId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetOrders()
        {
            var order = OrderBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(order);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.orders);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetOrders.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Orders.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetOrderById()
        {
            var order = OrderBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(order);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(order.OrderId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetOrderById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateOrder = "api/orders";
            }

            public static class Put
            {
                public static string Update = "api/orders";
            }

            public static class Delete
            {
                public static string By(Guid orderId)
                {
                    return $"api/orders/{orderId}";
                }
            }

            public static class Get
            {
                public static string orders = "api/orders";
                public static string By(Guid orderId)
                {
                    return $"api/orders/{orderId}";
                }
            }
        }
    }
}
