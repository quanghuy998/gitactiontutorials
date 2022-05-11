using TestOnlineProject.Domain.Aggregates.ExamAggregate;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using Xunit;

namespace TestOnineProejct.UnitTest.DomainTest
{
    public class ExamAggregateTest
    {
        [Fact]
        public void GivenInformation_WhenCreatingAnExam_ThenItShouldBeCreated()
        {
            string title = "Exam 1";

            var exam = new Exam(title);

            Assert.Equal(title, exam.Title);
            Assert.Empty(exam.Questions);
        }

        [Fact]
        public void GivenInformation_WhenUpdatingAnExam_ThenItShouldBeUpdated()
        {
            string title = "Exam 2";
            var exam = GivenSampleExam();

            exam.UpdateExam(title);

            Assert.Equal(title, exam.Title);
        }

        [Fact]
        public void GivenAnQuestion_WhenAddingAnQuestionToAnExam_ThenItShouldBeAdded()
        {
            var question = GivenSampleQuestion();
            var exam = GivenSampleExam();

            exam.AddQuestion(question);

            Assert.Contains(question, exam.Questions);
        }

        [Fact]
        public void GivenAnQuestion_WhenRemovingAnQuestionFromExam_ThenItShouldBeRemoved()
        {
            var question = GivenSampleQuestion();
            var exam = GivenSampleExam();

            exam.AddQuestion(question);
            exam.RemoveQuestion(question.Id);

            Assert.Empty(exam.Questions);
        }

        private Question GivenSampleQuestion()
        {
            string questionText = "Question number 1?";
            int point = 4;

            return new Question(questionText, point, QuestionType.MultipChoice);
        }

        private Exam GivenSampleExam()
        {
            string title = "Exam 1";
            return new Exam(title);
        }
    }
}
