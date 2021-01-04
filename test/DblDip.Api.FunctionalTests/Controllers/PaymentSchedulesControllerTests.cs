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
using static DblDip.Api.FunctionalTests.PaymentSchedulesControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class PaymentSchedulesControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public PaymentSchedulesControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreatePaymentSchedule()
        {
            var context = _fixture.Context;

            var paymentSchedule = PaymentScheduleDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { paymentSchedule }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreatePaymentSchedule, stringContent);

            var response = JsonConvert.DeserializeObject<CreatePaymentSchedule.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<PaymentSchedule>(response.PaymentSchedule.PaymentScheduleId);

            Assert.NotEqual(default, response.PaymentSchedule.PaymentScheduleId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemovePaymentSchedule()
        {
            var paymentSchedule = PaymentScheduleBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(paymentSchedule);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(paymentSchedule.PaymentScheduleId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedPaymentSchedule = await context.FindAsync<PaymentSchedule>(paymentSchedule.PaymentScheduleId);

            Assert.NotEqual(default, removedPaymentSchedule.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdatePaymentSchedule()
        {
            var paymentSchedule = PaymentScheduleBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(paymentSchedule);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { paymentSchedule = paymentSchedule.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<PaymentSchedule>(paymentSchedule.PaymentScheduleId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetPaymentSchedules()
        {
            var paymentSchedule = PaymentScheduleBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(paymentSchedule);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.PaymentSchedules);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetPaymentSchedules.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.PaymentSchedules.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetPaymentScheduleById()
        {
            var paymentSchedule = PaymentScheduleBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(paymentSchedule);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(paymentSchedule.PaymentScheduleId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetPaymentScheduleById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreatePaymentSchedule = "api/payment-schedules";
            }

            public static class Put
            {
                public static string Update = "api/payment-schedules";
            }

            public static class Delete
            {
                public static string By(Guid paymentScheduleId)
                {
                    return $"api/payment-schedules/{paymentScheduleId}";
                }
            }

            public static class Get
            {
                public static string PaymentSchedules = "api/payment-schedules";
                public static string By(Guid paymentScheduleId)
                {
                    return $"api/payment-schedules/{paymentScheduleId}";
                }
            }
        }
    }
}
