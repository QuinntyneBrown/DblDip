using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features;
using DblDip.Testing;
using DblDip.Testing.Builders;
using DblDip.Testing.Builders;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.PaymentsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class PaymentsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public PaymentsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreatePayment()
        {
            var context = _fixture.Context;

            var payment = PaymentDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { payment }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreatePayment, stringContent);

            var response = JsonConvert.DeserializeObject<CreatePayment.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Payment>(response.Payment.PaymentId);

            Assert.NotEqual(default, response.Payment.PaymentId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemovePayment()
        {
            var payment = PaymentBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(payment);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(payment.PaymentId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedPayment = await context.FindAsync<Payment>(payment.PaymentId);

            Assert.NotEqual(default, removedPayment.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdatePayment()
        {
            var payment = PaymentBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(payment);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { payment = payment.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Payment>(payment.PaymentId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetPayments()
        {
            var payment = PaymentBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(payment);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Payments);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetPayments.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Payments.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetPaymentById()
        {
            var payment = PaymentBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(payment);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(payment.PaymentId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetPaymentById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreatePayment = "api/payments";
            }

            public static class Put
            {
                public static string Update = "api/payments";
            }

            public static class Delete
            {
                public static string By(Guid paymentId)
                {
                    return $"api/payments/{paymentId}";
                }
            }

            public static class Get
            {
                public static string Payments = "api/payments";
                public static string By(Guid paymentId)
                {
                    return $"api/payments/{paymentId}";
                }
            }
        }
    }
}
