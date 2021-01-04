using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.Questionnaires;
using DblDip.Testing;
using DblDip.Testing.Builders;
using DblDip.Testing.Builders;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.QuestionnairesControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class QuestionnairesControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public QuestionnairesControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateQuestionnaire()
        {
            var context = _fixture.Context;

            var questionnaire = QuestionnaireDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { questionnaire }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateQuestionnaire, stringContent);

            var response = JsonConvert.DeserializeObject<CreateQuestionnaire.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Questionnaire>(response.Questionnaire.QuestionnaireId);

            Assert.NotEqual(default, response.Questionnaire.QuestionnaireId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveQuestionnaire()
        {
            var questionnaire = QuestionnaireBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(questionnaire);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(questionnaire.QuestionnaireId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedQuestionnaire = await context.FindAsync<Questionnaire>(questionnaire.QuestionnaireId);

            Assert.NotEqual(default, removedQuestionnaire.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateQuestionnaire()
        {
            var questionnaire = QuestionnaireBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(questionnaire);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { questionnaire = questionnaire.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Questionnaire>(questionnaire.QuestionnaireId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetQuestionnaires()
        {
            var questionnaire = QuestionnaireBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(questionnaire);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Questionnaires);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetQuestionnaires.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Questionnaires.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetQuestionnaireById()
        {
            var questionnaire = QuestionnaireBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(questionnaire);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(questionnaire.QuestionnaireId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetQuestionnaireById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateQuestionnaire = "api/questionnaires";
            }

            public static class Put
            {
                public static string Update = "api/questionnaires";
            }

            public static class Delete
            {
                public static string By(Guid questionnaireId)
                {
                    return $"api/questionnaires/{questionnaireId}";
                }
            }

            public static class Get
            {
                public static string Questionnaires = "api/questionnaires";
                public static string By(Guid questionnaireId)
                {
                    return $"api/questionnaires/{questionnaireId}";
                }
            }
        }
    }
}
