using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.Conversations;
using DblDip.Testing;
using DblDip.Testing.Builders;
using DblDip.Testing.Builders;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.ConversationsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class ConversationsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public ConversationsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateConversation()
        {
            var context = _fixture.Context;

            var conversation = ConversationDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { conversation }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateConversation, stringContent);

            var response = JsonConvert.DeserializeObject<CreateConversation.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Conversation>(response.Conversation.ConversationId);

            Assert.NotEqual(default, response.Conversation.ConversationId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveConversation()
        {
            var conversation = ConversationBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(conversation);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(conversation.ConversationId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedConversation = await context.FindAsync<Conversation>(conversation.ConversationId);

            Assert.NotEqual(default, removedConversation.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateConversation()
        {
            var conversation = ConversationBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(conversation);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { conversation = conversation.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Conversation>(conversation.ConversationId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetConversations()
        {
            var conversation = ConversationBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(conversation);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Conversations);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetConversations.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Conversations.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetConversationById()
        {
            var conversation = ConversationBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(conversation);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(conversation.ConversationId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetConversationById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateConversation = "api/conversations";
            }

            public static class Put
            {
                public static string Update = "api/conversations";
            }

            public static class Delete
            {
                public static string By(Guid conversationId)
                {
                    return $"api/conversations/{conversationId}";
                }
            }

            public static class Get
            {
                public static string Conversations = "api/conversations";
                public static string By(Guid conversationId)
                {
                    return $"api/conversations/{conversationId}";
                }
            }
        }
    }
}
