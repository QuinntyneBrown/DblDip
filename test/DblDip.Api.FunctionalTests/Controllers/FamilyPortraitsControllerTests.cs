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
using static DblDip.Api.FunctionalTests.FamilyPortraitsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class FamilyPortraitsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public FamilyPortraitsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateFamilyPortrait()
        {
            var context = _fixture.Context;

            var familyPortrait = FamilyPortraitDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { familyPortrait }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateFamilyPortrait, stringContent);

            var response = JsonConvert.DeserializeObject<CreateFamilyPortrait.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<FamilyPortrait>(response.FamilyPortrait.FamilyPortraitId);

            Assert.NotEqual(default, response.FamilyPortrait.FamilyPortraitId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveFamilyPortrait()
        {
            var familyPortrait = FamilyPortraitBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(familyPortrait);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(familyPortrait.FamilyPortraitId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedFamilyPortrait = await context.FindAsync<FamilyPortrait>(familyPortrait.FamilyPortraitId);

            Assert.NotEqual(default, removedFamilyPortrait.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateFamilyPortrait()
        {
            var familyPortrait = FamilyPortraitBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(familyPortrait);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { familyPortrait = familyPortrait.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<FamilyPortrait>(familyPortrait.FamilyPortraitId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetFamilyPortraits()
        {
            var familyPortrait = FamilyPortraitBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(familyPortrait);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.FamilyPortraits);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetFamilyPortraits.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.FamilyPortraits.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetFamilyPortraitById()
        {
            var familyPortrait = FamilyPortraitBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(familyPortrait);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(familyPortrait.FamilyPortraitId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetFamilyPortraitById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateFamilyPortrait = "api/family-portraits";
            }

            public static class Put
            {
                public static string Update = "api/family-portraits";
            }

            public static class Delete
            {
                public static string By(Guid familyPortraitId)
                {
                    return $"api/family-portraits/{familyPortraitId}";
                }
            }

            public static class Get
            {
                public static string FamilyPortraits = "api/family-portraits";
                public static string By(Guid familyPortraitId)
                {
                    return $"api/family-portraits/{familyPortraitId}";
                }
            }
        }
    }
}
