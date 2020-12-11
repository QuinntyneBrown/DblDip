using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.Referrals;
using DblDip.Testing;
using DblDip.Testing.Builders.Core.Models;
using DblDip.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.Controllers.ReferralsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests.Controllers
{
    public class ReferralsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public ReferralsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateReferral()
        {
            var context = _fixture.Context;

            var referral = ReferralDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { referral }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateReferral, stringContent);

            var response = JsonConvert.DeserializeObject<CreateReferral.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Referral>(response.Referral.ReferralId);

            Assert.NotEqual(default, response.Referral.ReferralId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveReferral()
        {
            var referral = ReferralBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(referral);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(referral.ReferralId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedReferral = await context.FindAsync<Referral>(referral.ReferralId);

            Assert.NotEqual(default, removedReferral.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateReferral()
        {
            var referral = ReferralBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(referral);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { referral = referral.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Referral>(referral.ReferralId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetReferrals()
        {
            var referral = ReferralBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(referral);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.referrals);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetReferrals.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Referrals.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetReferralById()
        {
            var referral = ReferralBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(referral);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(referral.ReferralId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetReferralById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateReferral = "api/referrals";
            }

            public static class Put
            {
                public static string Update = "api/referrals";
            }

            public static class Delete
            {
                public static string By(Guid referralId)
                {
                    return $"api/referrals/{referralId}";
                }
            }

            public static class Get
            {
                public static string referrals = "api/referrals";
                public static string By(Guid referralId)
                {
                    return $"api/referrals/{referralId}";
                }
            }
        }
    }
}
