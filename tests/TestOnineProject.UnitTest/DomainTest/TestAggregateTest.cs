using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using Xunit;

namespace TestOnineProject.UnitTest.DomainTest
{
    public class TestAggregateTest
    {
        [Fact]
        public void GivenInformation_WhenCreatingAnTest_ThenItShouldBeCreated()
        {
            string title = "Test 1";

            var test = new Test(title);

            Assert.Equal(title, test.Title);
            Assert.Empty(test.Questions);
        }

        [Fact]
        public void GivenInformation_WhenUpdatingAnTest_ThenItShouldBeUpdated()
        {
            string title = "Test 2";
            var test = GivenSampleTest();

            test.UpdateTest(title);

            Assert.Equal(title, test.Title);
        }

        [Fact]
        public void GivenAnQuestion_WhenAddingAnQuestionToAnTest_ThenItShouldBeAdded()
        {
            var question = GivenSampleQuestion();
            var test = GivenSampleTest();

            test.AddQuestion(question);

            Assert.Contains(question, test.Questions);
        }

        [Fact]
        public void GivenAnQuestion_WhenRemovingAnQuestionFromTest_ThenItShouldBeRemoved()
        {
            var question = GivenSampleQuestion();
            var test = GivenSampleTest();

            test.AddQuestion(question);
            test.RemoveQuestion(question.Id);

            Assert.Empty(test.Questions);
        }

        private Question GivenSampleQuestion()
        {
            string questionText = "Question number 1?";
            int point = 4;

            return new Question(questionText, point, QuestionType.MultipChoice);
        }

        private Test GivenSampleTest()
        {
            string title = "Test 1";
            return new Test(title);
        }
    }
}
