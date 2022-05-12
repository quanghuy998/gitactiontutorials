using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using Xunit;

namespace TestOnineProject.UnitTest.DomainTest
{
    public class QuestionAggregateTest
    {
        [Fact]
        public void GivenInformation_WhenCreatingAnQuestion_ThenItShouldBeCreated()
        {
            string questionText = "Question number 1?";
            int point = 4;

            var question = new Question(questionText, point, QuestionType.MultipChoice);

            Assert.Equal(point, question.Point);
            Assert.Equal(questionText, question.QuestionText);
            Assert.Equal(QuestionType.MultipChoice, question.QuestionType);
            Assert.Empty(question.Choices);
            Assert.Empty(question.Tests);
        }

        [Fact]
        public void GivenInformation_WhenUpdatingAnQuestion_ThenItShouldBeUpdated()
        {
            var question = GivenSampleQuestion();
            string questionText = "Question number 1?";
            int point = 4;

            question.UpdateQuestion(questionText, QuestionType.Code, point);

            Assert.Equal(point, question.Point);
            Assert.Equal(questionText, question.QuestionText);
            Assert.Equal(QuestionType.Code, question.QuestionType);
        }

        [Fact]
        public void GivenInformation_WhenCreatingAnChoice_ThenItShouldBeCreated()
        {
            string choiceText = "Choice 1";
            bool isCorrect = false;

            var choice = new Choice(choiceText, isCorrect);

            Assert.Equal(choiceText, choice.ChoiceText);
            Assert.Equal(isCorrect, choice.IsCorrect);
        }

        [Fact]
        public void GivenAnChoice_WhenAddingAnChoiceToQuestion_ThenItShouldBeAdded()
        {
            var question = GivenSampleQuestion();
            var choice = GivenSampleChoice();

            question.AddChoiceToQuestion(choice);

            Assert.Contains(choice, question.Choices);
        }

        [Fact]
        public void GivenAnChoice_WhenUpdatingAnChoiceToQuestion_ThenItShouldBeUpdating()
        {
            string choiceText = "Update choice";
            bool isCorrect = true;
            var question = GivenSampleQuestion();
            var choice = GivenSampleChoice();

            question.AddChoiceToQuestion(choice);
            choice.UpdateChoice(choiceText, isCorrect);
            question.UpdateChoiceInQuestion(choice);

            Assert.Contains(choice, question.Choices);
        }


        [Fact] 
        public void GivenAnChoice_WhenRemovingAnChoiceFromQuestion_ThenItShouldBeRemoved()
        {
            var question = GivenSampleQuestion();
            var choice = GivenSampleChoice();

            question.AddChoiceToQuestion(choice);
            question.RemoveChoiceFromQuestion(choice);

            Assert.Empty(question.Choices);
        }

        private Question GivenSampleQuestion()
        {
            string questionText = "Question number 1?";
            int point = 4;

            return new Question(questionText, point, QuestionType.MultipChoice);
        }

        private Choice GivenSampleChoice()
        {
            string choiceText = "Choice 1";
            bool isCorrect = false;

            return new Choice(choiceText, isCorrect);
        }
    }
}
