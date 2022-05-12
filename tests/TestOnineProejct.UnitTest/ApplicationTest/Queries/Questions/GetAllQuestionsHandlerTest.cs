using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestOnlineProject.Application.Queries.Questions;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Domain.SeedWork;
using Xunit;

namespace TestOnineProejct.UnitTest.ApplicationTest.Queries.Questions
{
    public class GetAllQuestionsHandlerTest
    {
        private readonly Mock<IQuestionRepository> mockQuestionRepository = new Mock<IQuestionRepository>();
        private GetAllQuestionsQuery query = new GetAllQuestionsQuery();

        [Fact]
        public async Task GivenRequest_WhenGetingAllQuestion_ThenItShouldReturnQuestionList()
        {
            var listQuestion = GivenSampleQuestionList();
            mockQuestionRepository.Setup(p => p.FindAllAsync(It.IsAny<BaseSpecification<Question>>(), It.IsAny<CancellationToken>())).ReturnsAsync(listQuestion);
            var handler = new GetAllQuestionsQueryHandler(mockQuestionRepository.Object);

            var result = await handler.Handle(query, CancellationToken.None);
            var resultList = result as List<Question>;

            Assert.Equal(listQuestion, resultList);
        }

        private List<Question> GivenSampleQuestionList()
        {
            return new List<Question>
            {
                new Question("Question test 1", 3, QuestionType.MultipChoice),
                new Question("Question test 2", 4, QuestionType.Code)
            };
        }
    }
}
