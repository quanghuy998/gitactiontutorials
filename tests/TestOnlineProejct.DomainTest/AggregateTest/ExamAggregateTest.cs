using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOnlineProject.Domain.Aggregates.ExamAggregate;
using Xunit;

namespace TestOnlineProejct.DomainTest.AggregateTest
{
    public class ExamAggregateTest
    {
        [Fact]
        public void GivenChoiceInformation_WhenCreatingAChoice_ThenItShouldBeCreated()
        {
            var choiceText = "What is this?";
            bool isCorrect = true;

            var choice = new Choice(choiceText, isCorrect);

            Assert.Equal(choiceText, choice.ChoiceText);
            Assert.Equal(isCorrect, choice.IsCorrect);
        }
    }
}
