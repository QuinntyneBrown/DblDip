using ShootQ.Core.Models;
using ShootQ.Core.ValueObjects;
using System.Linq;
using Xunit;

namespace ShootQ.Core.UnitTests.Models
{
    public class SurveyTests
    {

        [Fact]
        public void Should_Create()
        {
            var survey = new Survey("Test");

            Assert.NotEqual(default, survey.SurveyId);
            Assert.Empty(survey.SurveyQuestions);
        }

        [Fact]
        public void Should_AddQuestion()
        {
            var survey = new Survey("Test");
            survey.AddQuestion("How are the pics?");

            Assert.Single(survey.SurveyQuestions);
        }

        [Fact]
        public void Should_AddResult()
        {
            var survey = new Survey("Test");
            survey.AddQuestion("How would you rate the pics?");

            survey.AddSurveyResult((Email)"quinntynebrown@gmail.com",new[] {
                new Answer(survey.SurveyQuestions.First().QuestionId ,10)
            });

            Assert.Equal((Email)"quinntynebrown@gmail.com", survey.SurveyResults.Single().RespondentEmail);
            Assert.Single(survey.SurveyResults);
            Assert.Single(survey.SurveyResults.Single().Answers);
        }
    }
}
