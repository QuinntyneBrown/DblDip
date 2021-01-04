using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.CorporateEvents;
using DblDip.Testing;
using DblDip.Testing.Builders;
using DblDip.Testing.Builders;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.CorporateEventsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class CorporateEventsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public CorporateEventsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateCorporateEvent()
        {
            var context = _fixture.Context;

            var corporateEvent = CorporateEventDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { corporateEvent }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateCorporateEvent, stringContent);

            var response = JsonConvert.DeserializeObject<CreateCorporateEvent.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<CorporateEvent>(response.CorporateEvent.CorporateEventId);

            Assert.NotEqual(default, response.CorporateEvent.CorporateEventId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveCorporateEvent()
        {
            var corporateEvent = CorporateEventBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(corporateEvent);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(corporateEvent.CorporateEventId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedCorporateEvent = await context.FindAsync<CorporateEvent>(corporateEvent.CorporateEventId);

            //Assert.NotEqual(default, removedCorporateEvent.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateCorporateEvent()
        {
            var corporateEvent = CorporateEventBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(corporateEvent);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { corporateEvent = corporateEvent.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<CorporateEvent>(corporateEvent.CorporateEventId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetCorporateEvents()
        {
            var corporateEvent = CorporateEventBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(corporateEvent);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.corporateEvents);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetCorporateEvents.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.CorporateEvents.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetCorporateEventById()
        {
            var corporateEvent = CorporateEventBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(corporateEvent);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(corporateEvent.CorporateEventId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetCorporateEventById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateCorporateEvent = "api/corporate-events";
            }

            public static class Put
            {
                public static string Update = "api/corporate-events";
            }

            public static class Delete
            {
                public static string By(Guid corporateEventId)
                {
                    return $"api/corporate-events/{corporateEventId}";
                }
            }

            public static class Get
            {
                public static string corporateEvents = "api/corporate-events";
                public static string By(Guid corporateEventId)
                {
                    return $"api/corporate-events/{corporateEventId}";
                }
            }
        }
    }
}
