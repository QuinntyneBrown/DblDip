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
using static DblDip.Api.FunctionalTests.StudioPortraitsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class StudioPortraitsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public StudioPortraitsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateStudioPortrait()
        {
            var context = _fixture.Context;

            var studioPortrait = StudioPortraitDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { studioPortrait }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateStudioPortrait, stringContent);

            var response = JsonConvert.DeserializeObject<CreateStudioPortrait.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<StudioPortrait>(response.StudioPortrait.StudioPortraitId);

            Assert.NotEqual(default, response.StudioPortrait.StudioPortraitId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveStudioPortrait()
        {
            var studioPortrait = StudioPortraitBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(studioPortrait);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(studioPortrait.StudioPortraitId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedStudioPortrait = await context.FindAsync<StudioPortrait>(studioPortrait.StudioPortraitId);

            //Assert.NotEqual(default, removedStudioPortrait.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateStudioPortrait()
        {
            var studioPortrait = StudioPortraitBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(studioPortrait);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { studioPortrait = studioPortrait.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<StudioPortrait>(studioPortrait.StudioPortraitId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetStudioPortraits()
        {
            var studioPortrait = StudioPortraitBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(studioPortrait);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.studioPortraits);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetStudioPortraits.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.StudioPortraits.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetStudioPortraitById()
        {
            var studioPortrait = StudioPortraitBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(studioPortrait);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(studioPortrait.StudioPortraitId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetStudioPortraitById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateStudioPortrait = "api/studio-portraits";
            }

            public static class Put
            {
                public static string Update = "api/studio-portraits";
            }

            public static class Delete
            {
                public static string By(Guid studioPortraitId)
                {
                    return $"api/studio-portraits/{studioPortraitId}";
                }
            }

            public static class Get
            {
                public static string studioPortraits = "api/studio-portraits";
                public static string By(Guid studioPortraitId)
                {
                    return $"api/studio-portraits/{studioPortraitId}";
                }
            }
        }
    }
}
