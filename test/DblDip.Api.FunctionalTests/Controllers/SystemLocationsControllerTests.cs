using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.SystemLocations;
using DblDip.Testing;
using DblDip.Testing.Builders;
using DblDip.Testing.Builders;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.SystemLocationsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class SystemLocationsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public SystemLocationsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateSystemLocation()
        {
            var context = _fixture.Context;

            var systemLocation = SystemLocationDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { systemLocation }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateSystemLocation, stringContent);

            var response = JsonConvert.DeserializeObject<CreateSystemLocation.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<SystemLocation>(response.SystemLocation.SystemLocationId);

            Assert.NotEqual(default, response.SystemLocation.SystemLocationId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveSystemLocation()
        {
            var systemLocation = SystemLocationBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(systemLocation);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(systemLocation.SystemLocationId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedSystemLocation = await context.FindAsync<SystemLocation>(systemLocation.SystemLocationId);

            Assert.NotEqual(default, removedSystemLocation.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateSystemLocation()
        {
            var systemLocation = SystemLocationBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(systemLocation);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { systemLocation = systemLocation.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<SystemLocation>(systemLocation.SystemLocationId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetSystemLocations()
        {
            var systemLocation = SystemLocationBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(systemLocation);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.systemLocations);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetSystemLocations.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.SystemLocations.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetSystemLocationById()
        {
            var systemLocation = SystemLocationBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(systemLocation);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(systemLocation.SystemLocationId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetSystemLocationById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateSystemLocation = "api/system-locations";
            }

            public static class Put
            {
                public static string Update = "api/system-locations";
            }

            public static class Delete
            {
                public static string By(Guid systemLocationId)
                {
                    return $"api/system-locations/{systemLocationId}";
                }
            }

            public static class Get
            {
                public static string systemLocations = "api/system-locations";
                public static string By(Guid systemLocationId)
                {
                    return $"api/system-locations/{systemLocationId}";
                }
            }
        }
    }
}
