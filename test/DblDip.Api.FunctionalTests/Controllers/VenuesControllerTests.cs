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
using static DblDip.Api.FunctionalTests.VenuesControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class VenuesControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public VenuesControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateVenue()
        {
            var context = _fixture.Context;

            var venue = VenueDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { venue }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateVenue, stringContent);

            var response = JsonConvert.DeserializeObject<CreateVenue.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Venue>(response.Venue.VenueId);

            Assert.NotEqual(default, response.Venue.VenueId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveVenue()
        {
            var venue = VenueBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(venue);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(venue.VenueId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedVenue = await context.FindAsync<Venue>(venue.VenueId);

            Assert.NotEqual(default, removedVenue.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateVenue()
        {
            var venue = VenueBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(venue);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { venue = venue.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Venue>(venue.VenueId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetVenues()
        {
            var venue = VenueBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(venue);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Venues);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetVenues.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Venues.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetVenueById()
        {
            var venue = VenueBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(venue);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(venue.VenueId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetVenueById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateVenue = "api/venues";
            }

            public static class Put
            {
                public static string Update = "api/venues";
            }

            public static class Delete
            {
                public static string By(Guid venueId)
                {
                    return $"api/venues/{venueId}";
                }
            }

            public static class Get
            {
                public static string Venues = "api/venues";
                public static string By(Guid venueId)
                {
                    return $"api/venues/{venueId}";
                }
            }
        }
    }
}
