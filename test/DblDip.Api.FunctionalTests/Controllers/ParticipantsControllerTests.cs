using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.Participants;
using DblDip.Testing;
using DblDip.Testing.Builders.Core.Models;
using DblDip.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.Controllers.ParticipantsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests.Controllers
{
    public class ParticipantsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public ParticipantsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateParticipant()
        {
            var context = _fixture.Context;

            var participant = ParticipantDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { participant }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateParticipant, stringContent);

            var response = JsonConvert.DeserializeObject<CreateParticipant.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Participant>(response.Participant.ParticipantId);

            Assert.NotEqual(default, response.Participant.ParticipantId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveParticipant()
        {
            var participant = ParticipantBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(participant);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(participant.ParticipantId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedParticipant = await context.FindAsync<Participant>(participant.ParticipantId);

            Assert.NotEqual(default, removedParticipant.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateParticipant()
        {
            var participant = ParticipantBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(participant);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { participant = participant.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Participant>(participant.ParticipantId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetParticipants()
        {
            var participant = ParticipantBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(participant);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Participants);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetParticipants.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Participants.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetParticipantById()
        {
            var participant = ParticipantBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(participant);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(participant.ParticipantId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetParticipantById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateParticipant = "api/participants";
            }

            public static class Put
            {
                public static string Update = "api/participants";
            }

            public static class Delete
            {
                public static string By(Guid participantId)
                {
                    return $"api/participants/{participantId}";
                }
            }

            public static class Get
            {
                public static string Participants = "api/participants";
                public static string By(Guid participantId)
                {
                    return $"api/participants/{participantId}";
                }
            }
        }
    }
}
