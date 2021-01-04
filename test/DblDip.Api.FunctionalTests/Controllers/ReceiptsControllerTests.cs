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
using static DblDip.Api.FunctionalTests.ReceiptsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class ReceiptsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public ReceiptsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateReceipt()
        {
            var context = _fixture.Context;

            var receipt = ReceiptDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { receipt }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateReceipt, stringContent);

            var response = JsonConvert.DeserializeObject<CreateReceipt.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Receipt>(response.Receipt.ReceiptId);

            Assert.NotEqual(default, response.Receipt.ReceiptId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveReceipt()
        {
            var receipt = ReceiptBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(receipt);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(receipt.ReceiptId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedReceipt = await context.FindAsync<Receipt>(receipt.ReceiptId);

            Assert.NotEqual(default, removedReceipt.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateReceipt()
        {
            var receipt = ReceiptBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(receipt);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { receipt = receipt.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Receipt>(receipt.ReceiptId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetReceipts()
        {
            var receipt = ReceiptBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(receipt);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Receipts);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetReceipts.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Receipts.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetReceiptById()
        {
            var receipt = ReceiptBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(receipt);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(receipt.ReceiptId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetReceiptById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateReceipt = "api/receipts";
            }

            public static class Put
            {
                public static string Update = "api/receipts";
            }

            public static class Delete
            {
                public static string By(Guid receiptId)
                {
                    return $"api/receipts/{receiptId}";
                }
            }

            public static class Get
            {
                public static string Receipts = "api/receipts";
                public static string By(Guid receiptId)
                {
                    return $"api/receipts/{receiptId}";
                }
            }
        }
    }
}
