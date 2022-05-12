using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Queries.Questions;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using Xunit;

namespace TestOnineProject.UnitTest.ApplicationTest.Queries.Questions
{
    public class GetQuestionDetailsHandlerTest
    {
        private readonly Mock<IQuestionRepository> mockQuestionRepository = new Mock<IQuestionRepository>();
        private GetQuestionDetailsQuery query = new GetQuestionDetailsQuery() { Id = Guid.NewGuid() };

        [Fact]
        public async Task GivenInformation_WhenGettingQuestionDetails_ThenItShoudReturnQuestion()
        {
            var question = GivenSampleQuestion();
            mockQuestionRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(question);
            var handler = new GetQuestionDetailsQueryHandler(mockQuestionRepository.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(question, result);
        }

        [Fact]
        public async Task GivenInformation_WhenGettingDetailsOfTheQuestionDoesNotExist_ThenItShouldReturnNull()
        {
            mockQuestionRepository.Setup(p => p.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);
            var handler = new GetQuestionDetailsQueryHandler(mockQuestionRepository.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Null(result);
        }

        private Question GivenSampleQuestion()
        {
            string questionText = "Question number 1?";
            int point = 4;
            return new Question(questionText, point, QuestionType.MultipChoice);
        }
    }
}
