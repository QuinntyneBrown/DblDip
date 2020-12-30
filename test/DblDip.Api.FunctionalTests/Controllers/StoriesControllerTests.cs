using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.Stories;
using DblDip.Testing;
using DblDip.Testing.Builders.Core.Models;
using DblDip.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.Controllers.StoriesControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests.Controllers
{
    public class StoriesControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public StoriesControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateStory()
        {
            var context = _fixture.Context;

            var story = StoryDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { story }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateStory, stringContent);

            var response = JsonConvert.DeserializeObject<CreateStory.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Story>(response.Story.StoryId);

            Assert.NotEqual(default, response.Story.StoryId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveStory()
        {
            var story = StoryBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(story);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(story.StoryId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedStory = await context.FindAsync<Story>(story.StoryId);

            Assert.NotEqual(default, removedStory.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateStory()
        {
            var story = StoryBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(story);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { story = story.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Story>(story.StoryId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetStories()
        {
            var story = StoryBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(story);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Stories);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetStories.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Stories.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetStoryById()
        {
            var story = StoryBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(story);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(story.StoryId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetStoryById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateStory = "api/stories";
            }

            public static class Put
            {
                public static string Update = "api/stories";
            }

            public static class Delete
            {
                public static string By(Guid storyId)
                {
                    return $"api/stories/{storyId}";
                }
            }

            public static class Get
            {
                public static string Stories = "api/stories";
                public static string By(Guid storyId)
                {
                    return $"api/stories/{storyId}";
                }
            }
        }
    }
}
