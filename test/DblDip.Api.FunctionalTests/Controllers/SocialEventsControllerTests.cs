using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.SocialEvents;
using DblDip.Testing;
using DblDip.Testing.Builders.Core.Models;
using DblDip.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.Controllers.SocialEventsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests.Controllers
{
    public class SocialEventsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public SocialEventsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateSocialEvent()
        {
            var context = _fixture.Context;

            var socialEvent = SocialEventDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { socialEvent }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateSocialEvent, stringContent);

            var response = JsonConvert.DeserializeObject<CreateSocialEvent.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<SocialEvent>(response.SocialEvent.SocialEventId);

            Assert.NotEqual(default, response.SocialEvent.SocialEventId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveSocialEvent()
        {
            var socialEvent = SocialEventBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(socialEvent);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(socialEvent.SocialEventId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedSocialEvent = await context.FindAsync<SocialEvent>(socialEvent.SocialEventId);

            //Assert.NotEqual(default, removedSocialEvent.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateSocialEvent()
        {
            var socialEvent = SocialEventBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(socialEvent);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { socialEvent = socialEvent.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<SocialEvent>(socialEvent.SocialEventId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetSocialEvents()
        {
            var socialEvent = SocialEventBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(socialEvent);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.socialEvents);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetSocialEvents.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.SocialEvents.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetSocialEventById()
        {
            var socialEvent = SocialEventBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(socialEvent);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(socialEvent.SocialEventId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetSocialEventById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateSocialEvent = "api/social-events";
            }

            public static class Put
            {
                public static string Update = "api/social-events";
            }

            public static class Delete
            {
                public static string By(Guid socialEventId)
                {
                    return $"api/social-events/{socialEventId}";
                }
            }

            public static class Get
            {
                public static string socialEvents = "api/social-events";
                public static string By(Guid socialEventId)
                {
                    return $"api/social-events/{socialEventId}";
                }
            }
        }
    }
}
