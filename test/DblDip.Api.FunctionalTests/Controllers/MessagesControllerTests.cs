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
using static DblDip.Api.FunctionalTests.MessagesControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class MessagesControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public MessagesControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateMessage()
        {
            var context = _fixture.Context;

            var message = MessageDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { message }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateMessage, stringContent);

            var response = JsonConvert.DeserializeObject<CreateMessage.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Message>(response.Message.MessageId);

            Assert.NotEqual(default, response.Message.MessageId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveMessage()
        {
            var message = MessageBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(message);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(message.MessageId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedMessage = await context.FindAsync<Message>(message.MessageId);

            Assert.NotEqual(default, removedMessage.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateMessage()
        {
            var message = MessageBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(message);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { message = message.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Message>(message.MessageId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetMessages()
        {
            var message = MessageBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(message);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Messages);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetMessages.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Messages.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetMessageById()
        {
            var message = MessageBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(message);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(message.MessageId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetMessageById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateMessage = "api/messages";
            }

            public static class Put
            {
                public static string Update = "api/messages";
            }

            public static class Delete
            {
                public static string By(Guid messageId)
                {
                    return $"api/messages/{messageId}";
                }
            }

            public static class Get
            {
                public static string Messages = "api/messages";
                public static string By(Guid messageId)
                {
                    return $"api/messages/{messageId}";
                }
            }
        }
    }
}
