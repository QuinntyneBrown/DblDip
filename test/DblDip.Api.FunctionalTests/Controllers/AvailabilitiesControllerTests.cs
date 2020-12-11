using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.Availabilities;
using DblDip.Testing;
using DblDip.Testing.Builders.Core.Models;
using DblDip.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.Controllers.AvailabilitiesControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests.Controllers
{
    public class AvailabilitiesControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public AvailabilitiesControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateAvailability()
        {
            var context = _fixture.Context;

            var availability = AvailabilityDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { availability }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateAvailability, stringContent);

            var response = JsonConvert.DeserializeObject<CreateAvailability.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Availability>(response.Availability.AvailabilityId);

            Assert.NotEqual(default, response.Availability.AvailabilityId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveAvailability()
        {
            var availability = AvailabilityBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(availability);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(availability.AvailabilityId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedAvailability = await context.FindAsync<Availability>(availability.AvailabilityId);

            Assert.NotEqual(default, removedAvailability.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateAvailability()
        {
            var availability = AvailabilityBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(availability);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { availability = availability.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Availability>(availability.AvailabilityId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetAvailabilities()
        {
            var availability = AvailabilityBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(availability);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Availabilities);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetAvailabilities.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Availabilities.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetAvailabilityById()
        {
            var availability = AvailabilityBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(availability);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(availability.AvailabilityId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetAvailabilityById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateAvailability = "api/availabilities";
            }

            public static class Put
            {
                public static string Update = "api/availabilities";
            }

            public static class Delete
            {
                public static string By(Guid availabilityId)
                {
                    return $"api/availabilities/{availabilityId}";
                }
            }

            public static class Get
            {
                public static string Availabilities = "api/availabilities";
                public static string By(Guid availabilityId)
                {
                    return $"api/availabilities/{availabilityId}";
                }
            }
        }
    }
}
