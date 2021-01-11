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
using static DblDip.Api.FunctionalTests.ConsultationsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class ConsultationsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public ConsultationsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateConsultation()
        {
            var context = _fixture.Context;

            var consultation = ConsultationDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { consultation }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateConsultation, stringContent);

            var response = JsonConvert.DeserializeObject<CreateConsultation.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Consultation>(response.Consultation.ConsultationId);

            Assert.NotEqual(default, response.Consultation.ConsultationId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveConsultation()
        {
            var consultation = ConsultationBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Add(consultation);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(consultation.ConsultationId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedConsultation = await context.FindAsync<Consultation>(consultation.ConsultationId);

            Assert.NotEqual(default, removedConsultation.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateConsultation()
        {
            var consultation = ConsultationBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(consultation);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { consultation = consultation.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Consultation>(consultation.ConsultationId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetConsultations()
        {
            var consultation = ConsultationBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(consultation);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.consultations);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetConsultations.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Consultations.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetConsultationById()
        {
            var consultation = ConsultationBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(consultation);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(consultation.ConsultationId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetConsultationById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateConsultation = "api/consultations";
            }

            public static class Put
            {
                public static string Update = "api/consultations";
            }

            public static class Delete
            {
                public static string By(Guid consultationId)
                {
                    return $"api/consultations/{consultationId}";
                }
            }

            public static class Get
            {
                public static string consultations = "api/consultations";
                public static string By(Guid consultationId)
                {
                    return $"api/consultations/{consultationId}";
                }
            }
        }
    }
}
