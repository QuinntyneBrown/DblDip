using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.Leads;
using DblDip.Testing;
using DblDip.Testing.Builders;
using DblDip.Testing.Builders;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.LeadsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class LeadsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public LeadsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateLead()
        {
            var context = _fixture.Context;

            var lead = LeadDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { lead }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateLead, stringContent);

            var response = JsonConvert.DeserializeObject<CreateLead.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Lead>(response.Lead.LeadId);

            Assert.NotEqual(default, response.Lead.LeadId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveLead()
        {
            var lead = LeadBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(lead);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(lead.LeadId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedLead = await context.FindAsync<Lead>(lead.LeadId);

            Assert.NotEqual(default, removedLead.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateLead()
        {
            var lead = LeadBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(lead);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { lead = lead.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Lead>(lead.LeadId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetLeads()
        {
            var lead = LeadBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(lead);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.leads);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetLeads.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Leads.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetLeadById()
        {
            var lead = LeadBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(lead);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(lead.LeadId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetLeadById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateLead = "api/leads";
            }

            public static class Put
            {
                public static string Update = "api/leads";
            }

            public static class Delete
            {
                public static string By(Guid leadId)
                {
                    return $"api/leads/{leadId}";
                }
            }

            public static class Get
            {
                public static string leads = "api/leads";
                public static string By(Guid leadId)
                {
                    return $"api/leads/{leadId}";
                }
            }
        }
    }
}
