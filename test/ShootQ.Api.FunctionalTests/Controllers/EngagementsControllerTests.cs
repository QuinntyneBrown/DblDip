using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Domain.Features;
using ShootQ.Domain.Features.Engagements;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static ShootQ.Api.FunctionalTests.Controllers.EngagementsControllerTests.Endpoints;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class EngagementsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public EngagementsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateEngagement()
        {
            var context = _fixture.Context;

            var engagement = EngagementDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { engagement }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Post.CreateEngagement, stringContent);

            var response = JsonConvert.DeserializeObject<CreateEngagement.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Engagement>(response.Engagement.EngagementId);

            Assert.NotEqual(default, response.Engagement.EngagementId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveEngagement()
        {
            var engagement = EngagementBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(engagement);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(engagement.EngagementId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedEngagement = await context.FindAsync<Engagement>(engagement.EngagementId);

            Assert.NotEqual(default, removedEngagement.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateEngagement()
        {
            var engagement = EngagementBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(engagement);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { engagement = engagement.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Engagement>(engagement.EngagementId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetEngagements()
        {
            var engagement = EngagementBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(engagement);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Engagements);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetEngagements.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Engagements.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetEngagementById()
        {
            var engagement = EngagementBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(engagement);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(engagement.EngagementId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetEngagementById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateEngagement = "api/engagements";
            }

            public static class Put
            {
                public static string Update = "api/engagements";
            }

            public static class Delete
            {
                public static string By(Guid engagementId)
                {
                    return $"api/engagements/{engagementId}";
                }
            }

            public static class Get
            {
                public static string Engagements = "api/engagements";
                public static string By(Guid engagementId)
                {
                    return $"api/engagements/{engagementId}";
                }
            }
        }
    }
}
