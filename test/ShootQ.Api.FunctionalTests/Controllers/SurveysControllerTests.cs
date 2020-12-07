using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Testing;
using ShootQ.Testing.Builders;
using System;
using Xunit;
using BuildingBlocks.Core;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class SurveysControllerTests: IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public SurveysControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_CreateSurvey()
        {

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveSurvey()
        {   

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string AddSurvey = "api/surveys";
            }

            public static class Delete
            {
                public static string SurveyBy(Guid surveyId)
                {
                    return $"api/surveys/{surveyId}";
                }
            }
        }
    }
}
